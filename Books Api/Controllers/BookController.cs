using Books_Api.Application.Dto.Author;
using Books_Api.Application.Dto.Book;
using Books_Api.Application.Exceptions;
using Books_Api.Application.Interfaz;
using Books_Api.Application.Response;
using Books_Api.Application.Services;
using Books_Api.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService bookService = bookService;

    // Obtener un libro por su ID
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Book>>> GetBookById(int id)
    {
        try
        {
            var book = await bookService.GetBookById(id);
            var bookResponse = book.Adapt<BookResponseDto>();
            bookResponse.Author = book.Author.Adapt<AuthorResponseDto>();

            return Ok(new ApiResponse<BookResponseDto>(bookResponse, "Libro encontrado."));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponse<string>(string.Empty, "Error interno del servidor", false));
        }
    }

    // Crear un nuevo libro
    [HttpPost]
    public async Task<ActionResult<ApiResponse<Book>>> AddBook([FromBody] CreateBookDto bookDto)
    {
        try
        {
            // Agregamos el libro a la base de datos o cualquier otra lógica
            var book = await bookService.AddBook(bookDto);

            var bookResponse = book.Adapt<BookResponseDto>();

            bookResponse.Author = book.Author.Adapt<AuthorResponseDto>();

            // Devolver la respuesta
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id },
                new ApiResponse<BookResponseDto>(bookResponse, "Libro registrado exitosamente."));
        }
        catch (BusinessException ex)
        {
            return Conflict(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponse<string>(string.Empty, "Error interno del servidor", false));
        }
    }

    // Actualizar un libro
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<BookResponseDto>>> UpdateBook(int id, [FromBody] UpdateBookDto bookDto)
    {
        try
        {
            // Actualizar el libro utilizando el servicio
            var updatedBook = await bookService.UpdateBook(id, bookDto);

            // Adaptar la respuesta a un DTO (BookResponseDto)
            var bookResponse = updatedBook.Adapt<BookResponseDto>();
            bookResponse.Author = updatedBook.Author.Adapt<AuthorResponseDto>();

            // Devolver la respuesta con el libro actualizado
            return Ok(new ApiResponse<BookResponseDto>(bookResponse, "Libro actualizado correctamente."));
        }
        catch (BusinessException ex)
        {
            return Conflict(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponse<string>(string.Empty, "Error interno del servidor", false));
        }
    }


    // Eliminar un libro por su ID
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteBook(int id)
    {
        try
        {
            var result = await bookService.DeleteBook(id);
            return Ok(new ApiResponse<bool>(result, "Libro eliminado correctamente."));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new ApiResponse<string>(string.Empty, ex.Message, false));
        }
        catch (Exception)
        {
            return StatusCode(500, new ApiResponse<string>(string.Empty, "Error interno del servidor", false));
        }
    }
}
