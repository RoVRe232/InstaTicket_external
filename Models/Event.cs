using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Event
    {
        [Key]
        public string EventId { get; set; }

        [Required]
        [StringLength(256)]
        public string Location { get; set; }

        [Required]
        public int AvailableTickets { get; set; }

        [Required]
        public double Price { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        [StringLength(256)]
        public string PosterUrl { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
