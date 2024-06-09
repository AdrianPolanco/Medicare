using System.ComponentModel.DataAnnotations;

namespace Medicare.Presentation.Filters.Attributes
{
    public class IsDateInThePastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is DateOnly date)
            {
                if(date <= DateOnly.FromDateTime(DateTime.Now))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("La fecha de nacimiento debe ser en el pasado");
                }
            }

            return new ValidationResult("El valor ingresado no es una fecha válida");
        }
    }
}
