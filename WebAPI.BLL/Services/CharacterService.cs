using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DB;
using System.ComponentModel.DataAnnotations;
using WebAPI.DB.Guide;
using AutoMapper;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления персонажами.
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CharacterService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public CharacterService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Создает нового персонажа.
        /// </summary>
        /// <param name="character">Персонаж для создания.</param>
        /// <returns>Созданный персонаж.</returns>
        /// <exception cref="ArgumentException">Если модель не валидна.</exception>
        public async Task<Character> CreateCharacter(Character character)
        {
            var validationContext = new ValidationContext(character);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(character, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _context.Characters.Add(character);
            _context.SaveChanges();

            var questions = _context.Questions.ToList();
            foreach (var question in questions)
            {
                Answer answer = new Answer()
                {
                    CharacterId = character.Id,
                    QuestionId = question.Id,
                    AnswerText = ""
                };
                _context.Answers.Add(answer);
            }
            _context.SaveChanges();

            return character;
        }

        /// <summary>
        /// Обновляет существующего персонажа.
        /// </summary>
        /// <param name="characterWithAnswers">Персонаж с данными для обновления.</param>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <returns>Обновленный персонаж.</returns>
        /// <exception cref="KeyNotFoundException">Если персонаж не найден.</exception>
        public async Task<Character> UpdateCharacter(CharacterWithAnswers characterWithAnswers, int id)
        {
            // Получение персонажа из базы данных
            var character = _context.Characters.Find(id);
            if (characterWithAnswers.PictureId!=null)
            {
                character.PictureId = characterWithAnswers.PictureId;
            }
            if(characterWithAnswers.Name!=null)
            {
                character.Name=characterWithAnswers.Name;
            }
            // Обновление персонажа
            _context.Characters.Update(character);

            // Обновление блоков
            for (int i=1;i<characterWithAnswers.Answers.Length;i++) 
            {
                if (characterWithAnswers.Answers[i-1] != "")
                {
                    var answer = _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == i).FirstOrDefault();
                    if (answer == null)
                    {
                        throw new KeyNotFoundException();
                    }
                    answer.AnswerText = characterWithAnswers.Answers[i - 1];
                    _context.Answers.Update(answer);
                }
            }
            _context.SaveChanges();

            return character;
        }

        /// <summary>
        /// Удаляет персонажа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <returns>Удаленный персонаж.</returns>
        /// <exception cref="KeyNotFoundException">Если персонаж не найден.</exception>
        public async Task<Character> DeleteCharacter(int id)
        {
            var character = _context.Characters.Find(id);

            if (character == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление всех блоков персонажа
            foreach (var question in _context.Questions.ToList())
            {
                var answer = _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == question.Id).FirstOrDefault();
                if (answer == null)
                {
                    throw new KeyNotFoundException();
                }
                // Удаление блока
                _context.Answers.Remove(answer);
            }
            
 
            // Удаление всех добавленных атрибутов блока
            var addedAttributes = _context.AddedAttributes.Where(aa => aa.CharacterId == character.Id).ToList();
            foreach (var addedAttribute in addedAttributes)
            {
                _context.AddedAttributes.Remove(addedAttribute);
            }

            // Удаление всех записей в галерее
            var galleryItems = _context.BelongToGalleries.Where(gi => gi.CharacterId == id).ToList();
            foreach (var galleryItem in galleryItems)
            {
                // Удаление всех изображений
                var image = _context.Pictures.Find((int)galleryItem.PictureId);
                _context.Pictures.Remove(image);
                _context.BelongToGalleries.Remove(galleryItem);
            }

            // Удаление аватарки
            if (character.PictureId != null)
            {
                int idP = (int)character.PictureId;
                var imageavatar = _context.Pictures.Find(idP);
                _context.Pictures.Remove(imageavatar);
            }
            
            // Удаление связи
            var connections = _context.Connections.Where(c=> c.Character1Id==character.Id||c.Character2Id==character.Id).ToList();
            foreach (var connection in connections)
            {
                var belongToSchemes = _context.BelongToSchemes.Where(b=>b.ConnectionId==connection.Id).ToList();
                foreach(var belongToScheme in belongToSchemes)
                {
                    _context.BelongToSchemes.Remove(belongToScheme);
                }
                _context.Connections.Remove(connection);
            }
            _context.SaveChanges();

            // Удаление персонажа
            _context.Characters.Remove(character);
            _context.SaveChanges();

            return character;
        }

        /// <summary>
        /// Получает персонажа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <returns>Персонаж с заданным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если персонаж не найден.</exception>
        public async Task<CharacterWithAnswers> GetCharacter(int id)
        {
            var character = _context.Characters.Find(id);
            if (character == null)
            {
                throw new KeyNotFoundException();
            }

            string[] answers = new string[_context.Questions.ToList().Count()-1];
            foreach (var question in _context.Questions.ToList())
            {
                var answer = _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == question.Id).FirstOrDefault();
                if (answer == null)
                {
                    throw new KeyNotFoundException();
                }
                answers[question.Id - 1] = answer.AnswerText;
            }
            
            var characterWithAnswers = _mapper.Map<CharacterWithAnswers>(character);
            characterWithAnswers.Answers = answers;

            return characterWithAnswers;
        }

        /// <summary>
        /// Получает всех персонажей для указанной книги.
        /// </summary>
        /// <param name="idBook">Идентификатор книги.</param>
        /// <returns>Список персонажей с данными.</returns>
        public async Task<IEnumerable<CharacterAllData>> GetAllCharacters(int idBook)
        {
            var characters = _context.Characters.Where(c => c.BookId == idBook).ToList();
            var charactersAllData = new List<CharacterAllData>();
            
            foreach (var character in characters)
            {
                var characterAllData = _mapper.Map<CharacterAllData>(character);
                characterAllData.Name = character.Name;
                if (character.PictureId != null)
                    characterAllData.PictureContent = _context.Pictures.Find((int)character.PictureId).PictureContent;
                charactersAllData.Add(characterAllData);
            }
            return charactersAllData;
        }

        /// <summary>
        /// Получает все вопросы.
        /// </summary>
        /// <returns>Список всех вопросов.</returns>
        public async Task<IEnumerable<QuestionData>> GetQuestions()
        {
            var questions = _context.Questions.ToList();
            var questionsData = new List<QuestionData>();

            foreach (var question in questions)
            {
                var questionData = _mapper.Map<QuestionData>(question);
                questionData.Block = _context.NumberBlocks.Find(question.Block).Name;
                questionsData.Add(questionData);
            }

            return questionsData;
        }
    }
}
