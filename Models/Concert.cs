using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Concert : Event
    {
        [Required]
        [StringLength(256)]
        public string SingerName { get; set; }

    }
}
