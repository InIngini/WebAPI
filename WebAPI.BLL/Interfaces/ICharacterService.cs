﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(Character character);
        Task<Character> UpdateCharacter(CharacterWithAnswers character,int id);
        Task<Character> DeleteCharacter(int id);
        Task<CharacterWithAnswers> GetCharacter(int id);
        Task<IEnumerable<Character>> GetAllCharacters(int idBook);
    }

}