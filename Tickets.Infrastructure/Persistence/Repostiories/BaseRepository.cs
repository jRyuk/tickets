using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tickets.Domain.Entities;
using Tickets.Domain.Interfaces.Repositories;
using Tickets.Infrastructure.Pesistence;

namespace Tickets.Infrastructure.Persistence.Repostiories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if(result == 0 || entity.Id == 0)
            {
                return null;
            }

            return entity;
        }

        public async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return  _context.Set<TEntity>().Where(predicate).AsQueryable();
        }
    }
}
