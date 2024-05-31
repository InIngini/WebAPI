using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Timeline
    {
        [Key]
        public int idTimeline { get; set; }
        public string nameTimeline { get; set; }
        public int idBook { get; set; }

        [ForeignKey(nameof(idBook))]
        public Book Book { get; set; }
        
    }
}
