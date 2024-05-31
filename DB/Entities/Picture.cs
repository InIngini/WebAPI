using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Picture
    {
        [Key]
        public int idPicture { get; set; }
        public byte[] picture { get; set; }
    }
}
