using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Connection
    {
        [Key]
        public int IdConnection { get; set; }
        public int TypeConnection { get; set; }
        public int IdCharacter1 { get; set; }

        [ForeignKey(nameof(IdCharacter1))]
        public Character Character1 { get; set; }
        public int IdCharacter2 { get; set;}

        [ForeignKey(nameof(IdCharacter2))]
        public Character Character2 { get; set; }
    }
}
