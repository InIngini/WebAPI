using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DB.Entities
{
    [PrimaryKey(nameof(IdTimeline), nameof(IdEvent))]
    public class BelongToTimeline
    {
        public int IdTimeline { get; set; }

        [ForeignKey(nameof(IdTimeline))]
        public Timeline Timeline { get; set; }
        public int IdEvent { get; set; }

        [ForeignKey(nameof(IdEvent))]
        public Event Event { get; set; }
    }
}
