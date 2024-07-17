using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB;
using WebAPI.DB.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class CharacterRepository : IRepository<Character>
    {
        private Context db;

        public CharacterRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Character> GetAll(int id)
        {
            return db.Characters.Where(a => a.IdBook == id);
        }

        public Character Get(int id)
        {
            return db.Characters.Find(id);
        }

        public void Create(Character character)
        {
            db.Characters.Add(character);
        }

        public void Update(Character character)
        {
            db.Entry(character).State = EntityState.Modified;
        }

        public IEnumerable<Character> Find(Func<Character, Boolean> predicate)
        {
            return db.Characters.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Character character = db.Characters.Find(id);
            if (character != null)
                db.Characters.Remove(character);
        }
    }
}
