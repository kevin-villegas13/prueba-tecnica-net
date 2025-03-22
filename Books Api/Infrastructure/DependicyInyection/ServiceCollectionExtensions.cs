using Books_Api.Application.Interfaz;
using Books_Api.Application.Services;
using Books_Api.Domain.Interfaces;
using Books_Api.Infrastructure.Data;
using Books_Api.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace Books_Api.Infrastructure.DependicyInyection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Inyectar DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Conexion")));

        // Repositorios
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Servicios
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();

        return services;
    }
}
