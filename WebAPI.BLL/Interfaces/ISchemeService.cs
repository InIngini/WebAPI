using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

namespace WebAPI.BLL.Interfaces
{
    public interface ISchemeService
    {
        Task<Scheme> CreateScheme(Scheme scheme);
        Task<Scheme> UpdateScheme(Scheme scheme, int idConnection);
        Task<Scheme> DeleteScheme(int id);
        Task<Scheme> GetScheme(int id);
        Task<IEnumerable<Scheme>> GetAllSchemes(int idBook);
    }

}
