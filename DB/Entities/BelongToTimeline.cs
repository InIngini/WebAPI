using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    [PrimaryKey(nameof(idTimeline), nameof(idEvent))]
    public class BelongToTimeline
    {
        public int idTimeline { get; set; }

        [ForeignKey(nameof(idTimeline))]
        public Timeline Timeline { get; set; }
        public int idEvent { get; set; }

        [ForeignKey(nameof(idEvent))]
        public Event Event { get; set; }
    }
}
