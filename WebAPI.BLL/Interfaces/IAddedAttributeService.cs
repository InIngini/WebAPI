using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Interfaces
{
    public interface IAddedAttributeService
    {
        Task<AddedAttribute> CreateAddedAttribute(int id,AAData aa);
        Task<AddedAttribute> UpdateAddedAttribute(AddedAttribute addedAttribute);
        Task<AddedAttribute> DeleteAddedAttribute(int idc, int ida);
        Task<AddedAttribute> GetAddedAttribute(int id, CancellationToken cancellationToken);
        Task<IEnumerable<AddedAttribute>> GetAllAddedAttributes(int idCharacter, CancellationToken cancellationToken);
    }

}
