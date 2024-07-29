using AutoMapper;
using WebAPI.BLL.Mappings;
using WebAPI.DB.Entities;

namespace WebAPI.BLL.DTO
{
    public class EventData : IMapWith<Event>
    {
        public int? IdBook { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public int[]? IdCharacters { get; set; }
        
    }
}
