using InstaTicket.Models;
using InstaTicket.Models.RequestModels;
using InstaTicket.Repositories.Interfaces;
using InstaTicket.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InstaTicket.Services
{
    public class EventService : IEventService
    {
        private IConcertRepository _concertRepository;
        private IFestivalRepository _festivalRepository;
        private ICustomerRepository _customerRepository;
        public EventService(IConcertRepository concertRepository, IFestivalRepository festivalRepository,
            ICustomerRepository customerRepository)
        {
            _concertRepository = concertRepository;
            _festivalRepository = festivalRepository;
            _customerRepository = customerRepository;
        }
        public bool AddConcert(ConcertFormModel concertForm)
        {
            //search if same concert exists
            Concert concert = _concertRepository.GetQuery(
                e => e.Location == concertForm.Location &&
                e.SingerName == concertForm.SingerName &&
                e.StartDate.Equals(concertForm.StartDate))
                .FirstOrDefault();

            if (concert != null)
                return false;

            concert = new Concert
            {
                EventId = Guid.NewGuid().ToString(),
                SingerName = concertForm.SingerName,
                StartDate = concertForm.StartDate,
                Location = concertForm.Location,
                AvailableTickets = concertForm.AvailableTickets,
                Price = concertForm.Price,
                PosterUrl = concertForm.PosterUrl,
                Description = concertForm.Description
            };

            if (concertForm.BelongsToFestival)
            {
                Festival festival = _festivalRepository.GetById(concertForm.FestivalId);
                festival.Concerts.Add(concert);
                _festivalRepository.Update(festival);
            }

            _concertRepository.Add(concert);

            return true;
        }

        public bool RemoveConcert(string concertId)
        {
            Concert concert = _concertRepository
                .GetQuery(e=>e.EventId == concertId)
                .FirstOrDefault();

            if (concert != null)
                return false;

            _concertRepository.Delete(concert);

            return true;
        }

        public bool AddFestival(FestivalFormModel concertForm)
        {
            //check if festival exists
            Festival festival = _festivalRepository
                .GetQuery(e => e.Location == concertForm.Location &&
                e.StartDate.Equals(concertForm.StartDate) &&
                e.EndDate.Equals(concertForm.EndDate) &&
                e.Price == concertForm.Price)
                .FirstOrDefault();

            if (festival == null)
                return false;
            
            festival = new Festival
            {
                StartDate = concertForm.StartDate,
                EndDate = concertForm.EndDate,
                Location = concertForm.Location,
                AvailableTickets = concertForm.AvailableTickets,
                Price = concertForm.Price
            };

            _festivalRepository.Add(festival);

            return true;
        }

        public bool RemoveFestival(string festivalId)
        {
            Festival festival = _festivalRepository.GetById(festivalId);

            if (festival == null)
                return false;

            foreach(var concert in festival.Concerts)
                _concertRepository.Delete(concert);

            _festivalRepository.Delete(festival);

            return true;
        }

        public IEnumerable<Concert> AllConcerts()
        {
            return _concertRepository.GetAll();
        }

        public IEnumerable<Festival> AllFestivals()
        {
            return _festivalRepository.GetAll();
        }

        public async Task<ICollection<Concert>> GetUserConcerts(int userId)
        {
            var customer = _customerRepository
                .GetFullCustomerQuery(e => e.Id == userId)
                .FirstOrDefault();

            if (customer == null)
                return null;


            ICollection<Concert> concerts = new List<Concert>();
            foreach(var ticket in customer.HeldTickets)
            {
                var concert = _concertRepository
                    .GetQuery(e => ticket.TicketEvent.EventId == e.EventId)
                    .FirstOrDefault();
                if(concert == null)
                    continue;
                concerts.Add(concert);
            }

            return concerts;
        }

        public Event GetEventById(string eventId)
        {
            var concert = _concertRepository.GetById(eventId);
            if (concert != null)
                return concert;

            var festival = _festivalRepository.GetById(eventId);
            if (festival != null)
                return festival;

            return null;
        }
    }
}
