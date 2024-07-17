using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    public class Picture
    {
        [Key]
        public int IdPicture { get; set; }
        public byte[] Picture1 { get; set; }
    }
}
