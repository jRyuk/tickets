using Azure;
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
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext _context;

        public  BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            var result = await _context.SaveChangesAsync();

            if(result == 0 || entity.Id == 0)
            {
                return null;
            }

            return entity;
        }
        public virtual async Task<bool> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null) return false;

            entity.IsDeleted = true;
            
            return await _context.SaveChangesAsync() >= 1;
        }
        public virtual async Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return  _context.Set<TEntity>().Where(predicate).AsQueryable();
        }
        public virtual async Task<TEntity?> FindFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query =  _context.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, int take, int page, string search)
        {
            var skip = (page - 1) * take;

            var query = _context.Set<TEntity>().Where(predicate);

            //OFFSET= skip, Take = TOP
            return query.Skip(skip).Take(take).AsQueryable();
        }
    }
}
