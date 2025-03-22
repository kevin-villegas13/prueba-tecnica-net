using Books_Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books_Api.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Author Configuration
        modelBuilder.Entity<Author>()
            .Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Author>()
            .Property(a => a.CityOfOrigin)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Author>()
            .Property(a => a.Email)
            .IsRequired();

        modelBuilder.Entity<Author>()
            .HasIndex(a => a.Email)
            .IsUnique();

        // Book Configuration
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Book>()
            .Property(b => b.Year)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(b => b.Genre)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Book>()
            .Property(b => b.PageCount)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}