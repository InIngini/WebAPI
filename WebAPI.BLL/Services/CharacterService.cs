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

            // Сохранение блоков с персонажем
            Block1 block1 = new Block1()
            {
                IdCharacter = character.IdCharacter,
                Name = String.Empty,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,

            };
            _unitOfWork.Block1s.Create(block1);
            Block2 block2 = new Block2()
            {
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,
                Question7 = String.Empty,
                Question8 = String.Empty,
                Question9 = String.Empty,
            };
            _unitOfWork.Block2s.Create(block2);
            Block3 block3 = new Block3()
            {
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
                Question6 = String.Empty,
                Question7 = String.Empty,
                Question8 = String.Empty,
                Question9 = String.Empty,
                Question10 = String.Empty,
            };
            _unitOfWork.Block3s.Create(block3);
            Block4 block4 = new Block4()
            {
                IdCharacter = character.IdCharacter,
                Question1 = String.Empty,
                Question2 = String.Empty,
                Question3 = String.Empty,
                Question4 = String.Empty,
                Question5 = String.Empty,
            };
            _unitOfWork.Block4s.Create(block4);

            _unitOfWork.Save();

            return character;
        }

        public async Task<Character> UpdateCharacter(CharacterWithBlocks characterWithBlocks)
        {
            // Получение персонажа из базы данных
            var character = _unitOfWork.Characters.Get(characterWithBlocks.IdCharacter);
            character.IdPicture = characterWithBlocks.IdPicture;
            // Обновление персонажа
            _unitOfWork.Characters.Update(character);

            // Обновление блоков
            var block1 = _unitOfWork.Block1s.Get(character.IdCharacter);
            block1.Name = characterWithBlocks.Name;
            block1.Question1 = characterWithBlocks.Block1Question1;
            block1.Question2 = characterWithBlocks.Block1Question2;
            block1.Question3 = characterWithBlocks.Block1Question3;
            block1.Question4 = characterWithBlocks.Block1Question4;
            block1.Question5 = characterWithBlocks.Block1Question5;
            block1.Question6 = characterWithBlocks.Block1Question6;

            var block2 = _unitOfWork.Block2s.Get(character.IdCharacter);
            block2.Question1 = characterWithBlocks.Block2Question1;
            block2.Question2 = characterWithBlocks.Block2Question2;
            block2.Question3 = characterWithBlocks.Block2Question3;
            block2.Question4 = characterWithBlocks.Block2Question4;
            block2.Question5 = characterWithBlocks.Block2Question5;
            block2.Question6 = characterWithBlocks.Block2Question6;
            block2.Question7 = characterWithBlocks.Block2Question7;
            block2.Question8 = characterWithBlocks.Block2Question8;
            block2.Question9 = characterWithBlocks.Block2Question9;

            var block3 = _unitOfWork.Block3s.Get(character.IdCharacter);
            block3.Question1 = characterWithBlocks.Block3Question1;
            block3.Question2 = characterWithBlocks.Block3Question2;
            block3.Question3 = characterWithBlocks.Block3Question3;
            block3.Question4 = characterWithBlocks.Block3Question4;
            block3.Question5 = characterWithBlocks.Block3Question5;
            block3.Question6 = characterWithBlocks.Block3Question6;
            block3.Question7 = characterWithBlocks.Block3Question7;
            block3.Question8 = characterWithBlocks.Block3Question8;
            block3.Question9 = characterWithBlocks.Block3Question9;
            block3.Question10 = characterWithBlocks.Block3Question10;

            var block4 = _unitOfWork.Block1s.Get(character.IdCharacter);
            block4.Question1 = characterWithBlocks.Block4Question1;
            block4.Question2 = characterWithBlocks.Block4Question2;
            block4.Question3 = characterWithBlocks.Block4Question3;
            block4.Question4 = characterWithBlocks.Block4Question4;
            block4.Question5 = characterWithBlocks.Block4Question5;
            
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
            var block1s = _unitOfWork.Block1s.Find(b => b.IdCharacter == character.IdCharacter);
            foreach (var block in block1s)
            {
                // Удаление блока
                _unitOfWork.Block1s.Delete(block.IdCharacter);
            }
            var block2s = _unitOfWork.Block2s.Find(b => b.IdCharacter == character.IdCharacter);
            foreach (var block in block2s)
            {
                // Удаление блока
                _unitOfWork.Block2s.Delete(block.IdCharacter);
            }
            var block3s = _unitOfWork.Block3s.Find(b => b.IdCharacter == character.IdCharacter);
            foreach (var block in block3s)
            {
                // Удаление блока
                _unitOfWork.Block3s.Delete(block.IdCharacter);
            }
            var block4s = _unitOfWork.Block4s.Find(b => b.IdCharacter == character.IdCharacter);
            foreach (var block in block4s)
            {
                // Удаление блока
                _unitOfWork.Block4s.Delete(block.IdCharacter);
            }
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

        public async Task<Character> GetCharacter(int id)
        {
            var character = _unitOfWork.Characters.Get(id);

            if (character == null)
            {
                throw new KeyNotFoundException();
            }

            return character;
        }

        public async Task<IEnumerable<Character>> GetAllCharacters(int idBook)
        {
            var characters = _unitOfWork.Characters.Find(c => c.IdBook == idBook).ToList();

            return characters;
        }
    }
}
