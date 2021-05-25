using InstaTicket.Models;
using InstaTicket.Models.RequestModels;
using InstaTicket.Repositories.Interfaces;
using InstaTicket.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
        }

        public bool BuyTickets(CheckoutFormModel checkoutForm)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
