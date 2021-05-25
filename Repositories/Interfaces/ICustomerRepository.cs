using InstaTicket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InstaTicket.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> GetFullCustomerQuery(Expression<Func<Customer, bool>> expression);
    }
}
