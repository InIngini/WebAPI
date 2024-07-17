using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    [PrimaryKey(nameof(IdCharacter), nameof(IdEvent))]
    public class BelongToEvent
    {
        public int IdCharacter { get; set; }

        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }
        public int IdEvent { get; set; }

        [ForeignKey(nameof(IdEvent))]
        public Event Event { get; set; }
    }
}
