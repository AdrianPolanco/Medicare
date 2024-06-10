using Medicare.Domain.Entities.Base;

namespace Medicare.Domain.Repositories.Base
{
    public interface ISelectablePartialRepository<T> : ISelectableReadOnlyRepository<T>, IPartialRepository<T> where T : Entity
    {
    }
}
