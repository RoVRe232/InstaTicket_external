using InstaTicket.Context;
using InstaTicket.Models;
using InstaTicket.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Repositories
{
    public class FestivalRepository : BaseRepository<Festival>, IFestivalRepository
    {
        public FestivalRepository(InstaAppContext dbContext) : base(dbContext) { }
    }
}
