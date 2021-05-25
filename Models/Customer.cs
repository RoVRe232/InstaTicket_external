using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Customer : User
    {
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(128)]
        public string PhoneNumber { get; set; }

        public Address HomeAddress { get; set; }

        public ICollection<Ticket> HeldTickets { get; set; }

    }
}
