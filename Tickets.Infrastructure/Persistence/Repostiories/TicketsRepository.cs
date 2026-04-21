using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async override Task<Ticket> AddAsync(Ticket entity)
        {
            if (entity == null) throw new ArgumentNullException();

            if (entity.Hours == 0) return null;

            return await base.AddAsync(entity);
        }

        public async override Task<IQueryable<Ticket>> GetAll(Expression<Func<Ticket, bool>> predicate, int take, int skip, string search)
        {
            var query = _context.Set<Ticket>().Where(predicate);

            search = search?.ToLower() ?? string.Empty;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Title.ToLower().Contains(search) || c.Description.ToLower().Contains(search));
            }
            return query.Skip(skip).Take(take).AsQueryable();

        }
    }
}
