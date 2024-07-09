﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> CreateCharacter(Character character);
        Task<Character> UpdateCharacter(Character character);
        Task<Character> DeleteCharacter(int id);
        Task<Character> GetCharacter(int id);
        Task<IEnumerable<Character>> GetAllCharacters(int idBook);
    }

}
