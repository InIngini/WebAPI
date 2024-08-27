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
using WebAPI.Errors;

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
                throw new ArgumentException(TypesOfErrors.NoValidModel());
            }

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            var questions = await _context.Questions.ToListAsync();
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
            await _context.SaveChangesAsync();

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
            var character = await _context.Characters.FindAsync(id);
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
            for (int i=1;i<=characterWithAnswers.Answers.Length;i++) 
            {
                if (characterWithAnswers.Answers[i-1] != "")
                {
                    var answer = await _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == i).FirstOrDefaultAsync();
                    if (answer == null)
                    {
                        throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Персонаж", 1));
                    }
                    answer.AnswerText = characterWithAnswers.Answers[i - 1];
                    _context.Answers.Update(answer);
                }
            }
            await _context.SaveChangesAsync();

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
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Персонаж", 1));
            }

            // Удаление всех блоков персонажа
            foreach (var question in await _context.Questions.ToListAsync())
            {
                var answer = await _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == question.Id).FirstOrDefaultAsync();
                if (answer == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Ответ", 1));
                }
                // Удаление блока
                _context.Answers.Remove(answer);
            }
            
 
            // Удаление всех добавленных атрибутов блока
            var addedAttributes = await _context.AddedAttributes.Where(aa => aa.CharacterId == character.Id).ToListAsync();
            foreach (var addedAttribute in addedAttributes)
            {
                _context.AddedAttributes.Remove(addedAttribute);
            }

            // Удаление всех записей в галерее
            var galleryItems = await _context.BelongToGalleries.Where(gi => gi.CharacterId == id).ToListAsync();
            foreach (var galleryItem in galleryItems)
            {
                // Удаление всех изображений
                var image = await _context.Pictures.FindAsync((int)galleryItem.PictureId);
                _context.Pictures.Remove(image);
                _context.BelongToGalleries.Remove(galleryItem);
            }

            // Удаление аватарки
            if (character.PictureId != null)
            {
                int idP = (int)character.PictureId;
                var imageavatar = await _context.Pictures.FindAsync(idP);
                _context.Pictures.Remove(imageavatar);
            }
            
            // Удаление связи
            var connections = await _context.Connections.Where(c=> c.Character1Id==character.Id||c.Character2Id==character.Id).ToListAsync();
            foreach (var connection in connections)
            {
                var belongToSchemes = await _context.BelongToSchemes.Where(b=>b.ConnectionId==connection.Id).ToListAsync();
                foreach(var belongToScheme in belongToSchemes)
                {
                    _context.BelongToSchemes.Remove(belongToScheme);
                }
                _context.Connections.Remove(connection);
            }
            await _context.SaveChangesAsync();

            // Удаление персонажа
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return character;
        }

        /// <summary>
        /// Получает персонажа по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор персонажа.</param>
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Персонаж с заданным идентификатором.</returns>
        /// <exception cref="KeyNotFoundException">Если персонаж не найден.</exception>
        public async Task<CharacterWithAnswers> GetCharacter(int id, CancellationToken cancellationToken)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Персонаж", 1));
            }
            var questions = await _context.Questions.ToListAsync(cancellationToken);
            string[] answers = new string[questions.Count()];
            foreach (var question in questions)
            {
                var answer = await _context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == question.Id).FirstOrDefaultAsync(cancellationToken);
                if (answer == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NoFoundById("Ответ", 1));
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
        /// <param name="cancellationToken">Токен для отмены запроса.</param>
        /// <returns>Список персонажей с данными.</returns>
        public async Task<IEnumerable<CharacterAllData>> GetAllCharacters(int idBook, CancellationToken cancellationToken)
        {
            var characters = await _context.Characters.Where(c => c.BookId == idBook).ToListAsync(cancellationToken);
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
            var questions = await _context.Questions.ToListAsync();
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
