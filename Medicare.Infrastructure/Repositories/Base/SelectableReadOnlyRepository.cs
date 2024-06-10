

using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories.Base
{
    public class SelectableReadOnlyRepository<T> : ReadOnlyRepository<T>, ISelectableReadOnlyRepository<T> where T : Entity
    {
        public SelectableReadOnlyRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();
                if (filter != null)
                {
                    query = query.Where(filter);
                }
                // Aplicar includes opcionales
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }

                ICollection<T> entities = await query.ToListAsync(cancellationToken);
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
