using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Answer
    {
        [Key]
        public int idCharacter { get; set; }

        [ForeignKey(nameof(idCharacter))]
        public Character Character { get; set; }
        public string name { get; set; }
        public string answer1Personality { get; set; }
        public string answer2Personality { get; set; }
        public string answer3Personality { get; set; }
        public string answer4Personality { get; set; }
        public string answer5Personality { get; set; }
        public string answer6Personality { get; set; }
        public string answer1Appearance { get; set; }
        public string answer2Appearance { get; set; }
        public string answer3Appearance { get; set; }
        public string answer4Appearance { get; set; }
        public string answer5Appearance { get; set; }
        public string answer6Appearance { get; set; }
        public string answer7Appearance { get; set; }
        public string answer8Appearance { get; set; }
        public string answer9Appearance { get; set; }
        public string answer1Temperament { get; set; }
        public string answer2Temperament { get; set; }
        public string answer3Temperament { get; set; }
        public string answer4Temperament { get; set; }
        public string answer5Temperament { get; set; }
        public string answer6Temperament { get; set; }
        public string answer7Temperament { get; set; }
        public string answer8Temperament { get; set; }
        public string answer9Temperament { get; set; }
        public string answer10Temperament { get; set; }
        public string answer1ByHistory { get; set; }
        public string answer2ByHistory { get; set; }
        public string answer3ByHistory { get; set; }
        public string answer4ByHistory { get; set; }
        public string answer5ByHistory { get; set; }

    }
}
