using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Connection
    {
        [Key]
        public int idConnection { get; set; }
        public int typeConnection { get; set; }
        public int idCharacter1 { get; set; }

        [ForeignKey(nameof(idCharacter1))]
        public Character Character1 { get; set; }
        public int idCharacter2 { get; set;}

        [ForeignKey(nameof(idCharacter2))]
        public Character Character2 { get; set; }
    }
}
