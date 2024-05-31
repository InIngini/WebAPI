using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Book
    {
        [Key]
        public int idBook {  get; set; }
        public string nameBook { get; set; }
        public int? idPicture { get; set;}

        [ForeignKey(nameof(idPicture))]
        public Picture? Picture { get; set; }
    }
}
