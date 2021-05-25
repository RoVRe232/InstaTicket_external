using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public Event TicketEvent { get; set; }

        [Required]
        public Customer Holder { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(512)]
        public string Details { get; set; }

    }
}
