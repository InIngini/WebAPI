using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DAL.Guide
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Block {  get; set; }
        public NumberBlock NumberBlock { get; set; }
    }
}
