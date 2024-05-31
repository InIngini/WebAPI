using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    [PrimaryKey(nameof(idUser), nameof(idBook))]
    public class BelongToBook
    {
        public int idUser { get; set; }

        [ForeignKey(nameof(idUser))]
        public User User { get; set; }
        public int idBook { get; set; }

        [ForeignKey(nameof(idBook))]
        public Book Book { get; set; }
        public int typeBelong { get; set; }

    }
}
