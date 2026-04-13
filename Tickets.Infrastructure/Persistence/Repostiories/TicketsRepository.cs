using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces.Repositories;
using Tickets.Infrastructure.Pesistence;

namespace Tickets.Infrastructure.Persistence.Repostiories
{
    public class TicketsRepository : BaseRepository<Ticket>, ITickesRepository
    {
        public TicketsRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
        }
    }
}
