using System.ComponentModel.DataAnnotations;

namespace Books_Api.Application.Dto.Book;

public class CreateBookDto
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [MaxLength(255, ErrorMessage = "El título no puede superar los 255 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "El año de publicación es obligatorio.")]
    [Range(1000, 2100, ErrorMessage = "El año debe estar entre 1000 y 2100.")]
    public int Year { get; set; }

    [Required(ErrorMessage = "El género es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El género no puede superar los 100 caracteres.")]
    public string Genre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de páginas es obligatorio.")]
    [Range(1, 10000, ErrorMessage = "El número de páginas debe estar entre 1 y 10,000.")]
    public int PageCount { get; set; }

    [Required(ErrorMessage = "El ID del autor es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del autor debe ser un número positivo.")]
    public int AuthorId { get; set; }
}
