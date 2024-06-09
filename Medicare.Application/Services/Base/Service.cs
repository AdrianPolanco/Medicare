using Medicare.Application.Services.Interfaces.Base;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using System.Linq.Expressions;

namespace Medicare.Application.Services.Base
{
    public abstract class Service<T> : IService<T> where T : Entity
    {
        public readonly IRepository<T> _repository;
        protected Service(IRepository<T> repository)
        {
            _repository = repository;     
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                T addedEntity = await _repository.AddAsync(entity, cancellationToken);
                return addedEntity;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteAsync(id, cancellationToken);
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
        {
            try
            {
               return await _repository.GetByIdAsync(id, cancellationToken, includes); 
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public virtual async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken,
            Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                return await _repository.GetByPagesAsync(page, cancellationToken, filter, includes);
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
                int rows = await _repository.GetRowsCountAsync(cancellationToken);
                double pages = Math.Ceiling(rows / 10.0);
                return (int)pages;
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
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
