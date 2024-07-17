using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Block {  get; set; }

        [ForeignKey(nameof(Block))]
        public NumberBlock NumberBlock { get; set; }
    }
}
