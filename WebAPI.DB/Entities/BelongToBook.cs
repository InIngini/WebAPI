using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    [PrimaryKey(nameof(IdUser), nameof(IdBook))]
    public class BelongToBook
    {
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUser))]
        public User User { get; set; }
        public int IdBook { get; set; }

        [ForeignKey(nameof(IdBook))]
        public Book Book { get; set; }
        public int TypeBelong { get; set; }

    }
}
