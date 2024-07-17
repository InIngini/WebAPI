using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Character
    {
        [Key]
        public int IdCharacter { get; set; }
        public int IdBook { get; set; }

        [ForeignKey(nameof(IdBook))]
        public Book Book { get; set; }
        public int? IdPicture { get; set; }

        [ForeignKey(nameof(IdPicture))]
        public Picture? Picture { get; set; }

    }
}
