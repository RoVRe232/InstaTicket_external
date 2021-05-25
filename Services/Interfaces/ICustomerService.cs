using InstaTicket.Models;
using InstaTicket.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Services.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        bool RemoveCustomer(int customerId);
        bool BuyTickets(CheckoutFormModel checkoutForm);
    }
}
