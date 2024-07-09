using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface IAddedAttributeService
    {
        Task<AddedAttribute> CreateAddedAttribute(AddedAttribute addedAttribute);
        Task<AddedAttribute> UpdateAddedAttribute(AddedAttribute addedAttribute);
        Task<AddedAttribute> DeleteAddedAttribute(int id);
        Task<AddedAttribute> GetAddedAttribute(int id);
        Task<IEnumerable<AddedAttribute>> GetAllAddedAttributes(int idCharacter);
    }

}
