using Medicare.Domain.Entities;
using Medicare.Domain.Repositories;
using Medicare.Infrastructure.Context;
using Medicare.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medicare.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken, params Expression<Func<User, object>>[] includes)
        {
                try
                {
                    includes = new Expression<Func<User, object>>[]
                    {
                        u => u.Role,
                        u => u.Office,
                        u => u.OwnedOffice
                    };
                    User? user = await base.GetByIdAsync(id, cancellationToken, includes );

                    return user;
                }
                catch (Exception ex)
                {
                    
                    throw new Exception(ex.Message);
                }
        }

        public override async Task<ICollection<User>> GetByPagesAsync(int page, CancellationToken cancellationToken, Expression<Func<User, bool>> filter = null, params Expression<Func<User, object>>[] includes)
        {
            try
            {
                includes = new Expression<Func<User, object>>[]
                {
                    u => u.Role,
                    u => u.Office,
                    u => u.OwnedOffice
                };
                ICollection<User> users = await base.GetByPagesAsync(page, cancellationToken, filter, includes);

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }   

        public override async Task UpdateAsync(User entity, CancellationToken cancellationToken)
        {
            using (var transaction = await BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    User existingUser = await GetByIdAsync(entity.Id, cancellationToken);

                    if (existingUser == null)
                    {
                        // Manejar el caso en que el usuario no se encuentre en la base de datos
                        throw new Exception($"El usuario con Id {entity.Id} no fue encontrado en la base de datos.");
                    }

                    existingUser.Name = entity.Name;
                    existingUser.Lastname = entity.Lastname;
                    existingUser.Username = entity.Username;
                    existingUser.Email = entity.Email;
                    existingUser.Password = entity.Password;
                    existingUser.RoleId = entity.RoleId;

                    await base.UpdateAsync(existingUser, cancellationToken);
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

        public async Task<bool> UserExists(string username, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbSet.AnyAsync(u => u.Username == username, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
