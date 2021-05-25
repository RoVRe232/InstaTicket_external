using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models.RequestModels
{
    public class ConcertFormModel
    {
        public string Location { get; set; }
        public string SingerName { get; set; }
        public int AvailableTickets { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public DateTime StartDate { get; set; }
        public bool BelongsToFestival { get; set; }
        public int FestivalId { get; set; }
        public string Description { get; set; }
        public string PosterUrl { get; set; }
    }
}
