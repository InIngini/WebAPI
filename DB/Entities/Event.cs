using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Event
    {
        [Key]
        public int idEvent {  get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public string time { get; set; }
    }
}
