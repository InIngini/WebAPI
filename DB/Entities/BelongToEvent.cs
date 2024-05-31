using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    [PrimaryKey(nameof(idCharacter), nameof(idEvent))]
    public class BelongToEvent
    {
        public int idCharacter { get; set; }

        [ForeignKey(nameof(idCharacter))]
        public Character Character { get; set; }
        public int idEvent { get; set; }

        [ForeignKey(nameof(idEvent))]
        public Event Event { get; set; }
    }
}
