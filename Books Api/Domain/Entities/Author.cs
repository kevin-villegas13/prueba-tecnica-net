namespace Books_Api.Domain.Entities;

public class Author
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? CityOfOrigin { get; set; }
    public string? Email { get; set; }

    // Relationship: An author can have multiple books
    public List<Book> Books { get; set; } = [];
}

