using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Guide
{
    public class NumberBlock
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
