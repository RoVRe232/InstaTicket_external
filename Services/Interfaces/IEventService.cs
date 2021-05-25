using InstaTicket.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Services.Interfaces
{
    public interface IEventService
    {
        bool AddConcert(ConcertFormModel concertForm);
        bool RemoveConcert(string concertId);
        bool AddFestival(FestivalFormModel concertForm);
        bool RemoveFestival(string festivalId);
    }
}
