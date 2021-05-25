using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Festival : Event
    {
        public ICollection<Concert> Concerts { get; set; }

    }
}
