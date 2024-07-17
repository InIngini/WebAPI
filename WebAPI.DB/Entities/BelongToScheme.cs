using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    [PrimaryKey(nameof(IdScheme), nameof(IdConnection))]
    public class BelongToScheme
    {
        public int IdScheme { get; set; }

        [ForeignKey(nameof(IdScheme))]
        public Scheme Scheme { get; set; }
        public int IdConnection { get; set; }

        [ForeignKey(nameof(IdConnection))]
        public Connection Connection { get; set; }
    }
}
