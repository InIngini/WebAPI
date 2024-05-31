using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Gallery
    {
        [Key]
        public int? idPicture { get; set; }

        [ForeignKey(nameof(idPicture))]
        public Picture? Picture { get; set; }
     
        public int idCharacter { get; set; }

        [ForeignKey(nameof(idCharacter))]
        public Character Character { get; set; }
    }
}
