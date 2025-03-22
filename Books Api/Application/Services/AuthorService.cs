using Books_Api.Application.Dto.Author;
using Books_Api.Application.Exceptions;
using Books_Api.Application.Interfaz;
using Books_Api.Domain.Entities;
using Books_Api.Domain.Interfaces;
using Mapster;
using System.Globalization;

namespace Books_Api.Application.Services;
public class AuthorService(IGenericRepository<Author> authorRepository) : IAuthorService
{
    private readonly IGenericRepository<Author> _authorRepository = authorRepository;

    public async Task<Author> AddAuthor(CreateAuthorDto authorDto)
    {
        DateTime birthDate = DateTime.ParseExact(authorDto.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        var authors = await _authorRepository.GetAll();

        bool authorExists = authors.Any(a =>
            string.Equals(a.FullName?.Trim(), authorDto.FullName?.Trim(), StringComparison.OrdinalIgnoreCase) ||
            string.Equals(a.Email?.Trim(), authorDto.Email?.Trim(), StringComparison.OrdinalIgnoreCase) ||
            a.DateOfBirth.Date == birthDate.Date);

        if (authorExists)
            throw new BusinessException("El autor ya está registrado con los mismos datos.");

        var newAuthor = authorDto.Adapt<Author>();
        newAuthor.DateOfBirth = birthDate;

        return await _authorRepository.Add(newAuthor);
    }


    public async Task<bool> DeleteAuthor(int id)
    {
        var author = await _authorRepository.GetById(id) ?? throw new NotFoundException("El autor no existe.");
        await _authorRepository.Delete(id);
        return true;
    }

    public async Task<(IEnumerable<Author> Authors, int TotalCount)> GetAllAuthors(int pageNumber, int pageSize)
    {
        var (authors, totalCount) = await _authorRepository.GetPaged(pageNumber, pageSize);
        return (authors, totalCount);
    }

    public async Task<Author> GetAuthorById(int id)
    {
        var author = await _authorRepository.GetById(id);
        return author ?? throw new NotFoundException("El autor no existe.");
    }

    public async Task<Author> UpdateAuthor(int id, UpdateAuthorDto authorDto)
    {
        var author = await _authorRepository.GetById(id) ?? throw new NotFoundException("El autor no existe.");

        if (!DateTime.TryParseExact(authorDto.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
            throw new BusinessException("Formato de fecha inválido. Debe ser 'yyyy-MM-dd'.");

        var authors = await _authorRepository.GetAll();

        if (authors.Any(a => a.Id != id &&
            string.Equals(a.FullName?.Trim(), authorDto.FullName?.Trim(), StringComparison.OrdinalIgnoreCase) &&
            string.Equals(a.Email?.Trim(), authorDto.Email?.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            throw new BusinessException("Ya existe otro autor con el mismo nombre y correo.");
        }

        // Mapear con Mapster
        authorDto.Adapt(author);
        author.DateOfBirth = birthDate; 

        return await _authorRepository.Update(author);
    }
}
