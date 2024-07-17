using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Timeline
    {
        [Key]
        public int IdTimeline { get; set; }
        public string NameTimeline { get; set; }
        public int IdBook { get; set; }

        [ForeignKey(nameof(IdBook))]
        public Book Book { get; set; }
        
    }
}
