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

        public static Expression<Func<Doctor, bool>>? GetDoctorFilter(string searchValue, Guid officeId)
        {
            Expression<Func<Doctor, bool>>? searchFilter = d => true;
            searchFilter = searchFilter.And(d => d.OfficeId == officeId);
            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(d => d.Name.Contains(searchValue) || d.Lastname.Contains(searchValue) || d.Email.Contains(searchValue));
            }

            return searchFilter;
        }

        public static Expression<Func<LabTest, bool>>? GetLabTestFilter(string searchValue, Guid officeId)
        {
            Expression<Func<LabTest, bool>>? searchFilter = lt => true;
            searchFilter = searchFilter.And(lt => lt.OfficeId == officeId);
            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(lt => lt.Name.Contains(searchValue));
            }

            return searchFilter;
        }

        public static Expression<Func<Patient, bool>>? GetPatientFilter(string searchValue, Guid officeId)
        {
            Expression<Func<Patient, bool>>? searchFilter = p => true;
            searchFilter = searchFilter.And(p => p.OfficeId == officeId);
            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(p => p.Name.Contains(searchValue) || p.Lastname.Contains(searchValue) || p.IdentityCard.Contains(searchValue));
            }

            return searchFilter;
        }

        public static Expression<Func<Appointment, bool>>? GetAppointmentFilter(string searchValue, Guid officeId)
        {
            Expression<Func<Appointment, bool>>? searchFilter = a => true;
            searchFilter = searchFilter.And(a => a.OfficeId == officeId);
            // Filtrar por búsqueda
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchFilter = searchFilter.And(a => a.Patient.Name.Contains(searchValue) || a.Patient.Lastname.Contains(searchValue) || a.Doctor.Name.Contains(searchValue) || a.Doctor.Lastname.Contains(searchValue));
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
