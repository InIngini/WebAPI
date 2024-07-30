using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;
using Microsoft.EntityFrameworkCore;
using WebAPI.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace WebAPI.BLL.Services
{
    public class SchemeService : ISchemeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SchemeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Scheme> CreateScheme(SchemeData schemedata)
        {
            //Scheme scheme = new Scheme()
            //{
            //    IdBook = schemedata.IdBook,
            //    NameScheme = schemedata.NameScheme,
            //};
            var scheme = _mapper.Map<Scheme>(schemedata);
            var validationContext = new ValidationContext(scheme);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(scheme, validationContext, validationResults, true))
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.Schemes.Create(scheme);
            _unitOfWork.Save();

            return scheme;
        }

        public async Task<Scheme> UpdateScheme(Scheme scheme, int idConnection)
        {

            // Поиск соединения по указанному идентификатору
            var connection = _unitOfWork.Connections.Get(idConnection);

            // Если соединение не найдено, вернуть ошибку
            if (connection == null)
            {
                throw new KeyNotFoundException();
            }

            // Добавление соединения в схему
            var belongToScheme = new BelongToScheme()
            {
                IdConnection = idConnection,
                IdScheme = scheme.IdScheme
            };
            _unitOfWork.BelongToSchemes.Create(belongToScheme);
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
            var belongToSchemes = _unitOfWork.BelongToSchemes.Find(b=>b.IdScheme==id).ToList();
            foreach(var belongToScheme in belongToSchemes)
            {
                _unitOfWork.BelongToSchemes.Delete(id,belongToScheme.IdConnection);
            }
            _unitOfWork.Save();
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
            var schemes = _unitOfWork.Schemes.Find(s => s.IdBook == idBook).ToList();

            return schemes;
        }
    }

}
