namespace Books_Api.Application.Dto.Author;

public class AuthorResponseDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? CityOfOrigin { get; set; }
    public string? Email { get; set; }
}

