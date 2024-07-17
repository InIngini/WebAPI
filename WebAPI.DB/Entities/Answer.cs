using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Answer
    {
        [Key]
        public int IdCharacter { get; set; }

        [ForeignKey(nameof(IdCharacter))]
        public Character Character { get; set; }
        public string Name { get; set; }
        public string Answer1Personality { get; set; }
        public string Answer2Personality { get; set; }
        public string Answer3Personality { get; set; }
        public string Answer4Personality { get; set; }
        public string Answer5Personality { get; set; }
        public string Answer6Personality { get; set; }
        public string Answer1Appearance { get; set; }
        public string Answer2Appearance { get; set; }
        public string Answer3Appearance { get; set; }
        public string Answer4Appearance { get; set; }
        public string Answer5Appearance { get; set; }
        public string Answer6Appearance { get; set; }
        public string Answer7Appearance { get; set; }
        public string Answer8Appearance { get; set; }
        public string Answer9Appearance { get; set; }
        public string Answer1Temperament { get; set; }
        public string Answer2Temperament { get; set; }
        public string Answer3Temperament { get; set; }
        public string Answer4Temperament { get; set; }
        public string Answer5Temperament { get; set; }
        public string Answer6Temperament { get; set; }
        public string Answer7Temperament { get; set; }
        public string Answer8Temperament { get; set; }
        public string Answer9Temperament { get; set; }
        public string Answer10Temperament { get; set; }
        public string Answer1ByHistory { get; set; }
        public string Answer2ByHistory { get; set; }
        public string Answer3ByHistory { get; set; }
        public string Answer4ByHistory { get; set; }
        public string Answer5ByHistory { get; set; }

    }
}
