using InstaTicket.Context;
using InstaTicket.Models;
using InstaTicket.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(InstaAppContext dbContext) : base(dbContext) { }
    }
}
