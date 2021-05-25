using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaTicket.Models;
using InstaTicket.Models.RequestModels;
using InstaTicket.Repositories.Interfaces;
using InstaTicket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstaTicket.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private EventService _eventService;
        private IEventRepository _eventRepository;
        private ICustomerRepository _customerRepository;
        private UserManager<IdentityUser> _userManager;

        public EventsController(EventService eventService, UserManager<IdentityUser> userManager,
            ICustomerRepository customerRepository, IEventRepository eventRepository) {
            _eventService = eventService;
            _userManager = userManager;
            _customerRepository = customerRepository;
            _eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            // Display all existing events
            IEnumerable<Concert> concerts = _eventService.AllConcerts();
            IEnumerable<Festival> festivals = _eventService.AllFestivals();
            List<Event> events = new List<Event>();

            ViewBag.Concerts = concerts;
            ViewBag.Festivals = festivals;

            foreach (var concert in concerts) events.Add(concert);
            foreach (var festival in festivals) events.Add(festival);

            ViewBag.Events = events;

            return View();
        }

        public IActionResult CreateConcert()
        {
            // get all existing events
            IEnumerable<Concert> concerts = _eventService.AllConcerts();
            IEnumerable<Festival> festivals = _eventService.AllFestivals();

            ViewBag.Concerts = concerts;
            ViewBag.Festivals = festivals;

            return View();
        }

        public IActionResult CreateFestival()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddConcert(ConcertFormModel eventForm)
        {
            //Should only be made available to admins
            if (_eventService.AddConcert(eventForm))
                return RedirectToAction(controllerName: "Events", actionName: "Index");

            ViewBag.Message = "Add concert failed";
            return RedirectToAction(controllerName: "Events", actionName: "CreateConcert", routeValues: new { message="err"});
        }

        [HttpPost]
        public async Task<IActionResult> AddFestival(FestivalFormModel eventForm)
        {
            //Should only be made available to admins
            if(_eventService.AddFestival(eventForm))
                return RedirectToAction(controllerName: "Events", actionName: "Index");

            ViewBag.Message = "Add concert failed";
            return RedirectToAction(controllerName: "Events", actionName: "CreateConcert", routeValues: new { message = "err" });
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> RemoveEvent([FromBody] string eventId)
        {
            if (_eventService.RemoveConcert(eventId) || _eventService.RemoveFestival(eventId))
                return RedirectToAction(controllerName: "Events", actionName: "Index");

            ViewBag.Message = "remove event failed";
            return RedirectToAction(controllerName: "Events", actionName: "Index", routeValues: new { message = "err" });
        }

        [HttpPost]
        public IActionResult BuyTickets(BuyTicketFormModel ticketsRequest)
        {
            var username = _userManager.GetUserName(HttpContext.User);
            Customer currentCustomer = _customerRepository
                .GetFullCustomerQuery(e => e.EmailAddress == username)
                .FirstOrDefault();

            var selectedEvent = _eventService.GetEventById(ticketsRequest.EventId);
            if (selectedEvent != null)
            {
                if(ticketsRequest.Amount > selectedEvent.AvailableTickets)
                {
                    //TODO display error pop-up
                    return RedirectToAction(controllerName: "Events", actionName: "Index");
                }

                for(int i=0; i<ticketsRequest.Amount; i++)
                {
                    Ticket newTicket = new Ticket()
                    {
                        TicketEvent = selectedEvent,
                        Holder = currentCustomer,
                        Price = selectedEvent.Price,
                        Details = "none"
                    };
                    currentCustomer.HeldTickets.Add(newTicket);
                }
                selectedEvent.AvailableTickets -= ticketsRequest.Amount;

                _eventRepository.Update(selectedEvent);
                _customerRepository.Update(currentCustomer);

                return RedirectToAction(controllerName: "Tickets", actionName: "Index");
            }

            //TODO display error pop-up
            return RedirectToAction(controllerName: "Events", actionName: "Index");
        }
    }
}