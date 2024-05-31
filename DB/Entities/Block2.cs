using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Block2
    {
        [Key]
        public int idCharacter { get; set; }

        [ForeignKey(nameof(idCharacter))]
        public Character Character { get; set; }
        public string question1 { get; set; }
        public string question2 { get; set; }
        public string question3 { get; set; }
        public string question4 { get; set; }
        public string question5 { get; set; }
        public string question6 { get; set; }
        public string question7 { get; set; }
        public string question8 { get; set; }
        public string question9 { get; set; }

    }
}
