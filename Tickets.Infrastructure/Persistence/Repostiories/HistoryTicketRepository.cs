using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces.Repositories;
using Tickets.Infrastructure.Pesistence;

namespace Tickets.Infrastructure.Persistence.Repostiories
{
    public class HistoryTicketRepository : BaseRepository<HistoryTicket>, IHistoryTicketRepository
    {
        public HistoryTicketRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        
       
    }
}
