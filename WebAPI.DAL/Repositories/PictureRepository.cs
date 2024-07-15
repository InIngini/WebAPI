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
    public class PictureRepository : IRepository<Picture>
    {
        private Context db;

        public PictureRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Picture> GetAll(int id)
        {
            return db.Pictures;
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public void Create(Picture picture)
        {
            db.Pictures.Add(picture);
        }

        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
        }

        public IEnumerable<Picture> Find(Func<Picture, Boolean> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
        }
    }
}
