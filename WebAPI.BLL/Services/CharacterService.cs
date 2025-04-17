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
using WebAPI.BLL.Additional;

namespace WebAPI.BLL.Services
{
    /// <summary>
    /// Сервис для управления персонажами.
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private readonly IContext Context;
        private readonly IMapper Mapper;
        private DeletionRepository DeletionRepository;
        private CreationRepository CreationRepository;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CharacterService"/>.
        /// </summary>
        /// <param name="context">Юнит оф ворк для работы с репозиториями.</param>
        /// <param name="mapper">Объект для преобразования данных.</param>
        public CharacterService(IContext context, IMapper mapper, DeletionRepository deletionRepository, CreationRepository creationRepository)
        {
            Context = context;
            Mapper = mapper;
            DeletionRepository = deletionRepository;
            CreationRepository = creationRepository;
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
                throw new ArgumentException(TypesOfErrors.NotValidModel());
            }

            Context.Characters.Add(character);
            await Context.SaveChangesAsync();

            await CreationRepository.CreateAllAnswerByCharacter(character.Id, Context);

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
            var character = await Context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
            }
            if (characterWithAnswers.PictureId != null)
            {
                character.PictureId = characterWithAnswers.PictureId;
            }
            if (characterWithAnswers.Name != null)
            {
                character.Name = characterWithAnswers.Name;
            }
            // Обновление персонажа
            Context.Characters.Update(character);

            // Обновление блоков
            for (int i = 1; i <= characterWithAnswers.Answers.Length; i++)
            {
                if (characterWithAnswers.Answers[i - 1] != "")
                {
                    var answer = await Context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == i).FirstOrDefaultAsync();
                    if (answer == null)
                    {
                        throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
                    }
                    answer.AnswerText = characterWithAnswers.Answers[i - 1];
                    Context.Answers.Update(answer);
                }
            }
            await Context.SaveChangesAsync();

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
            var character = await Context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
            }

            // Удаление всех ответом персонажа
            await DeletionRepository.DeleteAllAnswerByCharacter(character.Id, Context);

            // Удаление всех добавленных атрибутов блока
            var addedAttributes = await Context.AddedAttributes.Where(aa => aa.CharacterId == character.Id).ToListAsync();
            foreach (var addedAttribute in addedAttributes)
            {
                Context.AddedAttributes.Remove(addedAttribute);
            }

            // Удаление всех записей в галерее
            var galleryItems = await Context.BelongToGalleries.Where(gi => gi.CharacterId == id).ToListAsync();
            foreach (var galleryItem in galleryItems)
            {
                await DeletionRepository.DeletePicture((int)galleryItem.PictureId, Context);
            }

            // Удаление аватарки
            if (character.PictureId != null)
            {
                await DeletionRepository.DeletePicture((int)character.PictureId, Context);
            }

            // Удаление связи
            var connections = await Context.Connections.Where(c => c.Character1Id == character.Id || c.Character2Id == character.Id).ToListAsync();
            foreach (var connection in connections)
            {
                await DeletionRepository.DeleteConnection(connection.Id, Context);
            }

            // Удаление персонажа
            Context.Characters.Remove(character);
            await Context.SaveChangesAsync();

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
            var character = await Context.Characters.FirstOrDefaultAsync(x => x.Id == id);
            if (character == null)
            {
                throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Персонаж", 1));
            }
            var questions = await Context.Questions.ToListAsync(cancellationToken);
            string[] answers = new string[questions.Count()];
            foreach (var question in questions)
            {
                var answer = await Context.Answers.Where(a => a.CharacterId == character.Id && a.QuestionId == question.Id).FirstOrDefaultAsync(cancellationToken);
                if (answer == null)
                {
                    throw new KeyNotFoundException(TypesOfErrors.NotFoundById("Ответ", 1));
                }
                answers[question.Id - 1] = answer.AnswerText;
            }

            var characterWithAnswers = Mapper.Map<CharacterWithAnswers>(character);
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
            var characters = await Context.Characters.Where(c => c.BookId == idBook).ToListAsync(cancellationToken);
            var charactersAllData = new List<CharacterAllData>();

            foreach (var character in characters)
            {
                var characterAllData = Mapper.Map<CharacterAllData>(character);
                characterAllData.Name = character.Name;
                if (character.PictureId != null)
                    characterAllData.PictureContent = (await Context.Pictures.FirstOrDefaultAsync(x => x.Id == (int)character.PictureId))?.PictureContent;
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
            var questions = await Context.Questions.ToListAsync();
            var questionsData = new List<QuestionData>();

            foreach (var question in questions)
            {
                var questionData = Mapper.Map<QuestionData>(question);
                questionData.Block = (await Context.NumberBlocks.FirstOrDefaultAsync(x => x.Id == question.Block))?.Name;
                questionsData.Add(questionData);
            }

            return questionsData;
        }
    }
}
