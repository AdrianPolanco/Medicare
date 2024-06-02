﻿
using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Repositories.Base
{
    public interface IPartialRepository<T> where T : Entity
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
    }
}