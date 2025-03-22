using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Books_Api.Application.Validations;

public class BirthDateValidator : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string birthDate ||
            !DateTime.TryParseExact(birthDate, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return new ValidationResult("La fecha debe estar en formato yyyy-MM-dd.");
        }

        return ValidationResult.Success;
    }
}

