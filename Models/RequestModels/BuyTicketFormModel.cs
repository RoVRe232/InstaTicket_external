using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models.RequestModels
{
    public class BuyTicketFormModel
    {
        public string EventId { get; set; }
        public int Amount { get; set; }
    }
}
