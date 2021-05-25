using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models.RequestModels
{
    public class FestivalFormModel
    {
        public string Location { get; set; }
        public int AvailableTickets { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
