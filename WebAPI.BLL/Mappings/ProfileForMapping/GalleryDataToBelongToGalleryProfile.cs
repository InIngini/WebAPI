using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.DTO;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.Mappings.ProfileForMapping
{
    public class GalleryDataToBelongToGalleryProfile : Profile
    {
        public GalleryDataToBelongToGalleryProfile()
        {
            CreateMap<GalleryData, BelongToGallery>(); // Если IdUser генерируется в базе данных, то можно игнорировать
        }
    }

}
