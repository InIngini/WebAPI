using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.EF;
using WebAPI.DAL.Entities;
using WebAPI.DAL.Interfaces;

namespace WebAPI.DAL.Repositories
{
    public class AddedAttributeRepository : IRepository<AddedAttribute>
    {
        private Context db;

        public AddedAttributeRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<AddedAttribute> GetAll(int id)
        {
            return db.AddedAttributes.Where(a=>a.IdCharacter==id);
        }

        public AddedAttribute Get(int id)
        {
            return db.AddedAttributes.Find(id);
        }

        public void Create(AddedAttribute addedAttribute)
        {
            db.AddedAttributes.Add(addedAttribute);
        }

        public void Update(AddedAttribute addedAttribute)
        {
            db.Entry(addedAttribute).State = EntityState.Modified;
        }

        public IEnumerable<AddedAttribute> Find(Func<AddedAttribute, Boolean> predicate)
        {
            return db.AddedAttributes.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            AddedAttribute addedAttribute = db.AddedAttributes.Find(id);
            if (addedAttribute != null)
                db.AddedAttributes.Remove(addedAttribute);
        }
    }
}
