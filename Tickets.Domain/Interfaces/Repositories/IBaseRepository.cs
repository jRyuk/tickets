using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Tickets.Domain.Entities;

namespace Tickets.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where  TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<IQueryable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
