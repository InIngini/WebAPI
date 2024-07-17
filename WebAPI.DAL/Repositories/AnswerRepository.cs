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
    public class AnswerRepository : IRepository<Answer>
    {
        private Context db;

        public AnswerRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Answer> GetAll(int id)
        {
            return db.Answers.Where(a => a.IdCharacter == id);
        }

        public Answer Get(int id)
        {
            return db.Answers.Find(id);
        }

        public void Create(Answer answer)
        {
            db.Answers.Add(answer);
        }

        public void Update(Answer answer)
        {
            db.Entry(answer).State = EntityState.Modified;
        }

        public IEnumerable<Answer> Find(Func<Answer, Boolean> predicate)
        {
            return db.Answers.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Answer answer = db.Answers.Find(id);
            if (answer != null)
                db.Answers.Remove(answer);
        }
    }
}
