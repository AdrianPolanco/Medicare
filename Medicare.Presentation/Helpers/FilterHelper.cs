using Medicare.Application.Enums;
using Medicare.Domain.Entities;
using Medicare.Domain.Entities.Base;
using System.Linq.Expressions;

namespace Medicare.Presentation.Helpers
{
    public static class FilterHelper
    {
        public static Expression<Func<User, bool>>? GetUserFilter(UserFilterOptions? filterOption, string searchValue, Guid officeId)
        {
            Expression<Func<User, bool>>? searchFilter = u => true;
            // Filtrar por opción de usuario
            switch (filterOption)
            {
                case UserFilterOptions.Admins:
                    searchFilter = searchFilter.And(u => u.Role.Name == "Administrador" && u.OfficeId == officeId);
                    break;
                case UserFilterOptions.Assistants:
                    searchFilter = searchFilter.And(u => u.Role.Name == "Asistente" && u.OfficeId == officeId);
                    break;
                case UserFilterOptions.All:
                default:
                    searchFilter = searchFilter.And(u => u.OfficeId == officeId);
                    // No se aplica ningún filtro adicional
                    break;
            }

            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(u => u.Name.Contains(searchValue) || u.Lastname.Contains(searchValue) || u.Username.Contains(searchValue));
            }

            return searchFilter;
        }

        public static Expression<Func<T, bool>>? GetFilter<T>(string searchValue, Guid officeId) where T: IEntityWithOffice
        {
            Expression<Func<T, bool>>? searchFilter = u => true;
            searchFilter = searchFilter.And(u => u.OfficeId == officeId);
            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(u => u.ToString().Contains(searchValue));
            }

            return searchFilter;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));
            var leftBody = Expression.Invoke(left, parameter);
            var rightBody = Expression.Invoke(right, parameter);
            var body = Expression.AndAlso(leftBody, rightBody);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
