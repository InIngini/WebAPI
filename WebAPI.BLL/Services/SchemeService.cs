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
    public class SchemeService : ISchemeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchemeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Scheme> CreateScheme(Scheme scheme)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Schemes.Create(scheme);
            _unitOfWork.Save();

            return scheme;
        }

        public async Task<Scheme> UpdateScheme(Scheme scheme)
        {
            _unitOfWork.Schemes.Update(scheme);
            _unitOfWork.Save();

            return scheme;
        }

        public async Task<Scheme> DeleteScheme(int id)
        {
            var scheme = _unitOfWork.Schemes.Get(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }

            _unitOfWork.Schemes.Delete(id);
            _unitOfWork.Save();

            return scheme;
        }

        public async Task<Scheme> GetScheme(int id)
        {
            var scheme = _unitOfWork.Schemes.Get(id);

            if (scheme == null)
            {
                throw new KeyNotFoundException();
            }

            return scheme;
        }

        public async Task<IEnumerable<Scheme>> GetAllSchemes(int idBook)
        {
            var schemes = await _unitOfWork.Schemes.Find(s => s.IdBook == idBook).ToListAsync();

            return schemes;
        }
    }

}
