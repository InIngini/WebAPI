using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Event
    {
        [Key]
        public int IdEvent {  get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
    }
}
