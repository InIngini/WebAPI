using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Character
    {
        [Key]
        public int idCharacter { get; set; }
        public int idBook { get; set; }

        [ForeignKey(nameof(idBook))]
        public Book Book { get; set; }
        public int? idPicture { get; set; }

        [ForeignKey(nameof(idPicture))]
        public Picture? Picture { get; set; }

    }
}
