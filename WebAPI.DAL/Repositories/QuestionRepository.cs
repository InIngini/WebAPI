using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Interfaces;
using WebAPI.DAL.Guide;
using WebAPI.DAL.EF;

namespace WebAPI.DAL.Repositories
{
    public class QuestionRepository : IRepository<Question>
    {
        private Context db;

        public QuestionRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Question> GetAll(int id)
        {
            return db.Questions;
        }

        public Question Get(int id)
        {
            return db.Questions.Find(id);
        }

        public void Create(Question question)
        {
            db.Questions.Add(question);
        }

        public void Update(Question question)
        {
            db.Entry(question).State = EntityState.Modified;
        }

        public IEnumerable<Question> Find(Func<Question, Boolean> predicate)
        {
            return db.Questions.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Question question = db.Questions.Find(id);
            if (question != null)
                db.Questions.Remove(question);
        }
    }

}
