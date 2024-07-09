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
            // Проверка валидности модели
            if (!ModelState.IsValid)
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

        public async Task<Character> UpdateCharacter(Character character)
        {
            _unitOfWork.Characters.Update(character);
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

            _unitOfWork.Characters.Delete(id);
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
            var characters = await _unitOfWork.Characters.Find(c => c.IdBook == idBook).ToListAsync();

            return characters;
        }
    }
}
