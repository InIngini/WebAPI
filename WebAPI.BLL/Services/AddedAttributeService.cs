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
            var validationContext = new ValidationContext(addedAttribute);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(addedAttribute, validationContext, validationResults, true))
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

        public async Task<AddedAttribute> DeleteAddedAttribute(int idc, int ida)
        {
            var addedAttribute = _unitOfWork.AddedAttributes.Find(a=> a.IdAttribute==ida && a.IdCharacter==idc).Single();

            if (addedAttribute == null)
            {
                throw new KeyNotFoundException("Атрибут не найден");
            }

            _unitOfWork.AddedAttributes.Delete(ida);
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
            var addedAttributes = _unitOfWork.AddedAttributes.Find(aa => aa.IdCharacter == idCharacter).ToList();

            return addedAttributes;
        }
    }

}
