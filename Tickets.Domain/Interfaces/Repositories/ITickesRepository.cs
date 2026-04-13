using System;
using System.Collections.Generic;
using System.Text;
using Tickets.Domain.Entities;

namespace Tickets.Domain.Interfaces.Repositories
{
    public interface ITickesRepository : IBaseRepository<Ticket>
    {
        
    }
}
