using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        
        public string login { get; set; }

        public string password { get; set; }
    }
}
