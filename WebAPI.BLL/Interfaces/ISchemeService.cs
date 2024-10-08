﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Entities;
using WebAPI.BLL.DTO;

namespace WebAPI.BLL.Interfaces
{
    public interface ISchemeService
    {
        Task<Scheme> CreateScheme(SchemeData schemedata);
        Task<Scheme> UpdateScheme(int idScheme, int idConnection);
        Task<Scheme> DeleteScheme(int id);
        Task<Scheme> GetScheme(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Scheme>> GetAllSchemes(int idBook,CancellationToken cancellationToken);
    }

}
