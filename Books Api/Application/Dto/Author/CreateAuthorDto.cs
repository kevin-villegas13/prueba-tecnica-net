using Books_Api.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace Books_Api.Application.Dto.Author;

public class CreateAuthorDto
{
    [Required(ErrorMessage = "El nombre completo es obligatorio.")]
    [MaxLength(100, ErrorMessage = "No puede exceder los 100 caracteres.")]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [BirthDateValidator] 
    public string BirthDate { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad de origen es obligatoria.")]
    [MaxLength(50, ErrorMessage = "No puede exceder los 50 caracteres.")]
    public string? CityOfOrigin { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
    public string? Email { get; set; }
}

