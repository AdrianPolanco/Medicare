using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories.Base
{
    public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : Entity
    {
        protected readonly DbSet<T> _dbSet;
        
        public ReadOnlyRepository(ApplicationDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                // Aplicar includes opcionales
                if (includes != null)
                {
                    query = includes.Aggregate(query, (current, include) => current.Include(include));
                }

                T? entity = await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            int pageSize = 10;
            IQueryable<T> query = _dbSet.AsQueryable();

            // Aplicar filtro si se proporciona
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Aplicar includes opcionales
            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Aplicar paginación
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            // Obtener los resultados
            return await query.ToListAsync(cancellationToken);
        }

        public virtual async Task<int> GetRowsCountAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _dbSet.CountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
