﻿

using Medicare.Domain.Entities.Base;
using Medicare.Domain.Repositories.Base;
using Medicare.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medicare.Infrastructure.Repositories.Base
{
    public class PartialRepository<T> : ReadOnlyRepository<T>, IPartialRepository<T> where T : Entity
    {
        protected readonly ApplicationDbContext _context;

        public PartialRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            var transaction = await BeginTransactionAsync(cancellationToken);
            { 
                try
                {
                    await _dbSet.AddAsync(entity, cancellationToken);
                    await SaveChangesAsync(cancellationToken);
                    await CommitAsync(cancellationToken);
                    return entity;
                }
                catch (Exception ex)
                {
                    await RollbackAsync(cancellationToken);
                    throw new Exception(ex.Message);
                }
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

		protected Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return _context.Database.BeginTransactionAsync();
        }

        protected async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.Database.CommitTransactionAsync(cancellationToken);
        }

        protected async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        protected async Task RollbackAsync(CancellationToken cancellationToken)
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.RollbackTransactionAsync(cancellationToken);
            }
        }
    }
}
