using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using WebAPI.DAL.Guide;

namespace WebAPI.BLL.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CharacterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Character> CreateCharacter(Character character)
        {
            var validationContext = new ValidationContext(character);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(character, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Characters.Create(character);
            _unitOfWork.Save();

            Answer answer = new Answer()
            {
                IdCharacter = character.IdCharacter,
                Name="",
                Answer1Personality = "",
                Answer2Personality = "",
                Answer3Personality = "",
                Answer4Personality = "",
                Answer5Personality = "",
                Answer6Personality = "",
                Answer1Appearance = "",
                Answer2Appearance = "",
                Answer3Appearance = "",
                Answer4Appearance = "",
                Answer5Appearance = "",
                Answer6Appearance = "",
                Answer7Appearance = "",
                Answer8Appearance = "",
                Answer9Appearance = "",
                Answer1Temperament = "",
                Answer2Temperament = "",
                Answer3Temperament = "",
                Answer4Temperament = "",
                Answer5Temperament = "",
                Answer6Temperament = "",
                Answer7Temperament = "",
                Answer8Temperament = "",
                Answer9Temperament = "",
                Answer10Temperament = "",
                Answer1ByHistory = "",
                Answer2ByHistory = "",
                Answer3ByHistory = "",
                Answer4ByHistory = "",
                Answer5ByHistory = ""
            };
            _unitOfWork.Answers.Create(answer);

            _unitOfWork.Save();

            return character;
        }

        public async Task<Character> UpdateCharacter(CharacterWithAnswers characterWithAnswers, int id)
        {
            // Получение персонажа из базы данных
            var character = _unitOfWork.Characters.Get(id);
            if (character.IdPicture!=null)
            {
                character.IdPicture = characterWithAnswers.IdPicture;
            }
            // Обновление персонажа
            _unitOfWork.Characters.Update(character);

            // Обновление блоков
            var answer = _unitOfWork.Answers.Get(id);
            if (answer != null)
            {
                answer.Name = characterWithAnswers.Name;
                answer.Answer1Personality = characterWithAnswers.Answer1Personality;
                answer.Answer2Personality = characterWithAnswers.Answer2Personality;
                answer.Answer3Personality = characterWithAnswers.Answer3Personality;
                answer.Answer4Personality = characterWithAnswers.Answer4Personality;
                answer.Answer5Personality = characterWithAnswers.Answer5Personality;
                answer.Answer6Personality = characterWithAnswers.Answer6Personality;
                answer.Answer1Appearance = characterWithAnswers.Answer1Appearance;
                answer.Answer2Appearance = characterWithAnswers.Answer2Appearance;
                answer.Answer3Appearance = characterWithAnswers.Answer3Appearance;
                answer.Answer4Appearance = characterWithAnswers.Answer4Appearance;
                answer.Answer5Appearance = characterWithAnswers.Answer5Appearance;
                answer.Answer6Appearance = characterWithAnswers.Answer6Appearance;
                answer.Answer7Appearance = characterWithAnswers.Answer7Appearance;
                answer.Answer8Appearance = characterWithAnswers.Answer8Appearance;
                answer.Answer9Appearance = characterWithAnswers.Answer9Appearance;
                answer.Answer1Temperament = characterWithAnswers.Answer1Temperament;
                answer.Answer2Temperament = characterWithAnswers.Answer2Temperament;
                answer.Answer3Temperament = characterWithAnswers.Answer3Temperament;
                answer.Answer4Temperament = characterWithAnswers.Answer4Temperament;
                answer.Answer5Temperament = characterWithAnswers.Answer5Temperament;
                answer.Answer6Temperament = characterWithAnswers.Answer6Temperament;
                answer.Answer7Temperament = characterWithAnswers.Answer7Temperament;
                answer.Answer8Temperament = characterWithAnswers.Answer8Temperament;
                answer.Answer9Temperament = characterWithAnswers.Answer9Temperament;
                answer.Answer10Temperament = characterWithAnswers.Answer10Temperament;
                answer.Answer1ByHistory = characterWithAnswers.Answer1ByHistory;
                answer.Answer2ByHistory = characterWithAnswers.Answer2ByHistory;
                answer.Answer3ByHistory = characterWithAnswers.Answer3ByHistory;
                answer.Answer4ByHistory = characterWithAnswers.Answer4ByHistory;
                answer.Answer5ByHistory = characterWithAnswers.Answer5ByHistory;
            }
            _unitOfWork.Answers.Update(answer);
            _unitOfWork.Save();

            return character;
        }

        public async Task<Character> DeleteCharacter(int id)
        {
            var character = _unitOfWork.Characters.Get(id);

            if (character == null)
            {
                throw new KeyNotFoundException();
            }

            // Удаление всех блоков персонажа
            var answer = _unitOfWork.Answers.Get(character.IdCharacter);
            // Удаление блока
            _unitOfWork.Answers.Delete(answer.IdCharacter);
            
            
            // Удаление всех добавленных атрибутов блока
            var addedAttributes = _unitOfWork.AddedAttributes.Find(aa => aa.IdCharacter == character.IdCharacter);
            foreach (var addedAttribute in addedAttributes)
            {
                _unitOfWork.AddedAttributes.Delete(addedAttribute.IdAttribute);
            }

            // Удаление всех записей в галерее
            var galleryItems = _unitOfWork.Galleries.Find(gi => gi.IdCharacter == id);
            foreach (var galleryItem in galleryItems)
            {
                // Удаление всех изображений
                var image = _unitOfWork.Pictures.Get(galleryItem.IdPicture);
                _unitOfWork.Pictures.Delete(image.IdPicture);
                _unitOfWork.Galleries.Delete(galleryItem.IdPicture);
            }

            // Удаление аватарки
            if (character.IdPicture != null)
            {
                int idP = (int)character.IdPicture;
                var imageavatar = _unitOfWork.Pictures.Get(idP);
                _unitOfWork.Pictures.Delete(imageavatar.IdPicture);
            }
            
            // Удаление персонажа
            _unitOfWork.Characters.Delete(character.IdCharacter);
            _unitOfWork.Save();

            return character;
        }

        public async Task<CharacterWithAnswers> GetCharacter(int id)
        {
            var character = _unitOfWork.Characters.Get(id);
            var answer = _unitOfWork.Answers.Get(id);
            if (character == null)
            {
                throw new KeyNotFoundException();
            }
            var characterWithAnswers = new CharacterWithAnswers()
            {
                IdPicture = character.IdPicture,
                Name = answer.Name,
                Answer1Personality = answer.Answer1Personality,
                Answer2Personality = answer.Answer2Personality, 
                Answer3Personality = answer.Answer3Personality,
                Answer4Personality = answer.Answer4Personality,
                Answer5Personality = answer.Answer6Personality,
                Answer6Personality = answer.Answer6Personality,
                Answer1Appearance = answer.Answer1Appearance,
                Answer2Appearance = answer.Answer2Appearance,
                Answer3Appearance = answer.Answer3Appearance,
                Answer4Appearance = answer.Answer4Appearance,
                Answer5Appearance = answer.Answer5Appearance,
                Answer6Appearance = answer.Answer6Appearance,
                Answer7Appearance = answer.Answer7Appearance,
                Answer8Appearance = answer.Answer8Appearance,
                Answer9Appearance = answer.Answer9Appearance,
                Answer1Temperament = answer.Answer1Temperament,
                Answer2Temperament = answer.Answer2Temperament,
                Answer3Temperament = answer.Answer3Temperament,
                Answer4Temperament = answer.Answer4Temperament,
                Answer5Temperament = answer.Answer5Temperament,
                Answer6Temperament = answer.Answer6Temperament,
                Answer7Temperament = answer.Answer7Temperament,
                Answer8Temperament = answer.Answer8Temperament,
                Answer9Temperament = answer.Answer9Temperament,
                Answer10Temperament = answer.Answer10Temperament,
                Answer1ByHistory = answer.Answer1ByHistory,
                Answer2ByHistory = answer.Answer2ByHistory,
                Answer3ByHistory = answer.Answer3ByHistory,
                Answer4ByHistory = answer.Answer4ByHistory,
                Answer5ByHistory = answer.Answer5ByHistory
            };

            return characterWithAnswers;
        }

        public async Task<IEnumerable<CharacterAllData>> GetAllCharacters(int idBook)
        {
            var characters = _unitOfWork.Characters.Find(c => c.IdBook == idBook).ToList();
            var charactersAllData = new List<CharacterAllData>();
            foreach (var character in characters)
            {
                var characterAllData = new CharacterAllData()
                {
                    IdCharacter = character.IdCharacter,
                    Name = _unitOfWork.Answers.Get(character.IdCharacter).Name,
                };
                if (character.IdPicture != null)
                    characterAllData.Picture1 = _unitOfWork.Pictures.Get((int)character.IdPicture).Picture1;
                charactersAllData.Add(characterAllData);
            }
            return charactersAllData;
        }
        public async Task<IEnumerable<QuestionData>> GetQuestions()
        {
            var questions = _unitOfWork.Questions.GetAll(0).ToList();
            var questionsData = new List<QuestionData>();
            foreach (var question in questions)
            {
                var questionData = new QuestionData()
                {
                    Id = question.Id,
                    Name = question.Name,
                    Block = _unitOfWork.NumberBlocks.Get(question.Block).Name
                };
                questionsData.Add(questionData);
            }

            return questionsData;
        }
    }
}
