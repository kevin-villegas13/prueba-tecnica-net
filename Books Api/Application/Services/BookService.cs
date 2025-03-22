using Books_Api.Application.Dto.Book;
using Books_Api.Application.Exceptions;
using Books_Api.Application.Interfaz;
using Books_Api.Domain.Entities;
using Books_Api.Domain.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Books_Api.Application.Services;

public class BookService(IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository) : IBookService
{
    private readonly int _maxBooksAllowed = 5;

    public async Task<Book> AddBook(CreateBookDto bookDto)
    {
        // Verificar si el autor existe
        var author = await authorRepository.GetById(bookDto.AuthorId) ?? throw new NotFoundException("El autor no está registrado.");

        // Verificar si se ha alcanzado el número máximo de libros
        var booksByAuthor = await bookRepository.Find(b => b.AuthorId == bookDto.AuthorId);
        
        if (booksByAuthor.Count() >= _maxBooksAllowed)
            throw new BusinessException("No es posible registrar el libro, se alcanzó el máximo permitido.");

        var newBook = bookDto.Adapt<Book>();

        return await bookRepository.Add(newBook);
    }

    public async Task<bool> DeleteBook(int id)
    {
        var book = await bookRepository.GetById(id) ?? throw new NotFoundException("El libro no existe.");
        await bookRepository.Delete(book.Id);
        return true;
    }

    public async Task<Book> GetBookById(int id) => await bookRepository.GetByIdWithRelations(id, query => query.Include(b => b.Author)) ?? throw new NotFoundException("No se encontró el libro.");
    
    public async Task<Book> UpdateBook(int id, UpdateBookDto bookDto)
    {
        var book = await bookRepository.GetById(id) ?? throw new NotFoundException("El libro no existe.");

        // Verificar si el autor existe
        var author = await authorRepository.GetById(bookDto.AuthorId) ?? throw new NotFoundException("El autor no está registrado.");

        // Contar los libros que tiene el autor
        var booksByAuthor = await bookRepository.Find(b => b.AuthorId == bookDto.AuthorId);

        // Verificar si se ha alcanzado el número máximo de libros
        if (booksByAuthor.Count() >= _maxBooksAllowed && book.AuthorId != bookDto.AuthorId)
            throw new BusinessException("No es posible registrar el libro, se alcanzó el máximo permitido.");

        // Mapear el DTO al libro existente
        bookDto.Adapt(book);

        // Actualizar el libro
        return await bookRepository.Update(book);
    }
}

