using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;

namespace Medicare.Application.Services.Base
{
    public abstract class PartialService<T> : IPartialService<T> where T : Entity
    {
        protected readonly IPartialRepository<T> _partialRepository;
        protected PartialService(IPartialRepository<T> partialRepository) {
            _partialRepository = partialRepository;
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            try
            {
                await _partialRepository.AddAsync(entity, cancellationToken);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
