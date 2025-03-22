using Books_Api.Application.Dto.Author;
using Books_Api.Application.Exceptions;
using Books_Api.Application.Interfaz;
using Books_Api.Application.Response;
using Books_Api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Books_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    private readonly IAuthorService _authorService = authorService;


    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<Author>>>> GetAllAuthors([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var (authors, totalCount) = await _authorService.GetAllAuthors(pageNumber, pageSize);
        return Ok(new ApiResponse<object>(new { authors, totalCount }, "Lista de autores obtenida correctamente."));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<Author>>> GetAuthorById(int id)
    {
        var author = await _authorService.GetAuthorById(id);
        return Ok(new ApiResponse<Author>(author, "Autor encontrado."));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<Author>>> AddAuthor([FromBody] CreateAuthorDto authorDto)
    {
        try
        {
            var author = await _authorService.AddAuthor(authorDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id },
                new ApiResponse<Author>(author, "Autor registrado exitosamente."));
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

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<Author>>> UpdateAuthor(int id, [FromBody] UpdateAuthorDto authorDto)
    {
        try
        {
            var updatedAuthor = await _authorService.UpdateAuthor(id, authorDto);
            return Ok(new ApiResponse<Author>(updatedAuthor, "Autor encontrado."));
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

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteAuthor(int id)
    {
        await _authorService.DeleteAuthor(id);
        return Ok(new ApiResponse<bool>(true, "Autor eliminado correctamente."));
    }
}

