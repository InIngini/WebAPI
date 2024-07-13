using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class AddedAttribute
    {
        [Key]
        public int idAttribute {  get; set; }

        public int numberAnswer { get; set; }
        public string nameAttribute {  get; set; }
        public string contentAttribute { get; set; }

        public int idCharacter { get; set; }

        [ForeignKey(nameof(idCharacter))]
        public Character Character { get; set; }
    }
}
