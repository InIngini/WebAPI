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
    public class GalleryRepository : IRepository<Gallery>
    {
        private Context db;

        public GalleryRepository(Context context)
        {
            this.db = context;
        }

        public IEnumerable<Gallery> GetAll()
        {
            return db.Galleries;
        }

        public Gallery Get(int id)
        {
            return db.Galleries.Find(id);
        }

        public void Create(Gallery gallery)
        {
            db.Galleries.Add(gallery);
        }

        public void Update(Gallery gallery)
        {
            db.Entry(gallery).State = EntityState.Modified;
        }

        public IEnumerable<Gallery> Find(Func<Gallery, Boolean> predicate)
        {
            return db.Galleries.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            if (gallery != null)
                db.Galleries.Remove(gallery);
        }
    }
}
