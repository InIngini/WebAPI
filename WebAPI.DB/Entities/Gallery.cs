using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Gallery
    {
        [Key]
        public int? IdPicture { get; set; }

        [ForeignKey(nameof(IdPicture))]
        public Picture? Picture { get; set; }
     
        public int IdCharacter { get; set; }

        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }
    }
}
