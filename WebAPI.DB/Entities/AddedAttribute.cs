using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class AddedAttribute
    {
        [Key]
        public int IdAttribute {  get; set; }
        public int NumberAnswer { get; set; }
        public string NameAttribute {  get; set; }
        public string ContentAttribute { get; set; }
        public int IdCharacter { get; set; }

        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }
    }
}
