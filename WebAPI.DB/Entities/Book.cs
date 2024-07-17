using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Book
    {
        [Key]
        public int IdBook {  get; set; }
        public string NameBook { get; set; }
        public int? IdPicture { get; set;}

        [ForeignKey(nameof(IdPicture))]
        public Picture? Picture { get; set; }
    }
}
