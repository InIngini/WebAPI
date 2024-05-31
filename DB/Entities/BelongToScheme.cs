using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    [PrimaryKey(nameof(idScheme), nameof(idConnection))]
    public class BelongToScheme
    {
        public int idScheme { get; set; }

        [ForeignKey(nameof(idScheme))]
        public Scheme Scheme { get; set; }
        public int idConnection { get; set; }

        [ForeignKey(nameof(idConnection))]
        public Connection Connection { get; set; }
    }
}
