using Books_Api.Application.Dto.Author;
using Books_Api.Domain.Entities;
namespace Books_Api.Application.Response;

public class BookResponseDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Genre { get; set; }
    public int PageCount { get; set; }
    public AuthorResponseDto Author { get; set; } = new();
}

