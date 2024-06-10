

using Medicare.Application.Services.Base;
using Medicare.Application.Services.Interfaces;
using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using System.Linq.Expressions;

namespace Medicare.Application.Services
{
    public class LabTestResultService : Service<LabTestResult>, ILabTestResultService
    {
        public LabTestResultService(ILabTestResultRepository repository) : base(repository)
        {
        }

        public async Task<LabTestResult?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<LabTestResult, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, cancellationToken, includes);
        }

        public async Task<ICollection<LabTestResult>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<LabTestResult, bool>> filter = null, params Expression<Func<LabTestResult, object>>[] includes)
        {
            return await _repository.GetByPagesAsync(page, cancellationToken, filter, includes);
        }

    }   
}
