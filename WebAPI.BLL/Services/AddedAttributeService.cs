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
    public class AddedAttributeService : IAddedAttributeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddedAttributeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddedAttribute> CreateAddedAttribute(AddedAttribute addedAttribute)
        {
            // Проверка валидности модели
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Модель не валидна");
            }

            _unitOfWork.AddedAttributes.Create(addedAttribute);
            _unitOfWork.Save();

            return addedAttribute;
        }

        public async Task<AddedAttribute> UpdateAddedAttribute(AddedAttribute addedAttribute)
        {
            _unitOfWork.AddedAttributes.Update(addedAttribute);
            _unitOfWork.Save();

            return addedAttribute;
        }

        public async Task<AddedAttribute> DeleteAddedAttribute(int id)
        {
            var addedAttribute = _unitOfWork.AddedAttributes.Get(id);

            if (addedAttribute == null)
            {
                throw new KeyNotFoundException();
            }

            _unitOfWork.AddedAttributes.Delete(id);
            _unitOfWork.Save();

            return addedAttribute;
        }

        public async Task<AddedAttribute> GetAddedAttribute(int id)
        {
            var addedAttribute = _unitOfWork.AddedAttributes.Get(id);

            if (addedAttribute == null)
            {
                throw new KeyNotFoundException();
            }

            return addedAttribute;
        }

        public async Task<IEnumerable<AddedAttribute>> GetAllAddedAttributes(int idCharacter)
        {
            var addedAttributes = await _unitOfWork.AddedAttributes.Find(aa => aa.IdCharacter == idCharacter).ToListAsync();

            return addedAttributes;
        }
    }

}
