using InstaTicket.Models;
using InstaTicket.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Controllers
{
    public class TicketsController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private ICustomerRepository _customerRepository;

        public TicketsController(UserManager<IdentityUser> userManager,
            ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _customerRepository = customerRepository;
        }
        public IActionResult Index()
        {
            var username = _userManager.GetUserName(HttpContext.User);
            ViewBag.Username = username;

            Customer currentCustomer = _customerRepository
                .GetFullCustomerQuery(e => e.EmailAddress == username)
                .FirstOrDefault();

            List<Ticket> upcomingTickets = currentCustomer.HeldTickets.ToList();
            upcomingTickets.RemoveAll(e => e.TicketEvent.StartDate < DateTime.Now);
            ViewBag.UpcomingTickets = upcomingTickets.OrderBy(e => e.TicketEvent.StartDate);
            ViewBag.ComingSoonTickets = upcomingTickets.OrderBy(e => e.TicketEvent.StartDate).Take(3);

            List<Ticket> spentTickets = currentCustomer.HeldTickets.ToList();
            spentTickets.RemoveAll(e => e.TicketEvent.StartDate >= DateTime.Now);
            ViewBag.SpentTickets = upcomingTickets.OrderByDescending(e => e.TicketEvent.StartDate);

            return View();
        }

    }
}
