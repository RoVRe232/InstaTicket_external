using InstaTicket.Context;
using InstaTicket.Models;
using InstaTicket.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InstaTicket.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InstaAppContext dbContext) : base(dbContext) { }

        public IQueryable<Customer> GetFullCustomerQuery(Expression<Func<Customer, bool>> expression)
        {
            return dbContext.Customers
                .Include(e => e.HeldTickets)
                .ThenInclude(e=>e.TicketEvent)
                .Where(expression);
        }

    }
}
