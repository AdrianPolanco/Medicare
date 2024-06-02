using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;

namespace Medicare.Application.Services.Base
{
    public abstract class Service<T> : IService<T> where T : Entity
    {
        public readonly IRepository<T> _repository;
        protected Service(IRepository<T> repository)
        {
            _repository = repository;     
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.AddAsync(entity, cancellationToken);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.DeleteAsync(id, cancellationToken);
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
               return await _repository.GetByIdAsync(id, cancellationToken); 
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<T>> GetByPagesAsync(int page, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetByPagesAsync(page, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetRowsCountAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetRowsCountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
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
