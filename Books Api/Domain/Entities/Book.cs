namespace Books_Api.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Genre { get; set; }
    public int PageCount { get; set; }
    public int AuthorId { get; set; }

    public Author? Author { get; set; }
}

