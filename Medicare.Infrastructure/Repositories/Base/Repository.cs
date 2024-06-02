using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories.Base
{
    public abstract class Repository<T> : PartialRepository<T>, IRepository<T> where T : Entity
    {
        protected readonly IReadOnlyRepository<T> _readOnlyRepository;
        public Repository(ApplicationDbContext context): base(context)
        {
            _readOnlyRepository = new ReadOnlyRepository<T>(context);
        }
        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    T entity = await GetByIdAsync(id, cancellationToken);
                    entity.Deleted = true;
                    base._dbSet.Update(entity);
                    await SaveChangesAsync(cancellationToken);
                    await CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message);
                }
            }
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return await _readOnlyRepository.GetByIdAsync(id, cancellationToken, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return await _readOnlyRepository.GetByPagesAsync(page, cancellationToken, filter, includes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<int> GetRowsCountAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _readOnlyRepository.GetRowsCountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual T DetachEntity(T entity, CancellationToken cancellationToken)
        {
            if (_context.Entry(entity).State != EntityState.Detached) _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
