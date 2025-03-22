# Books API - Prueba Técnica

## Descripción
Este proyecto es una API desarrollada en **.NET Framework 4.5 o superior** con **C#** y **SQL Server** como base de datos. Su función principal es gestionar libros y autores, permitiendo su registro y controlando ciertas reglas de negocio.

## Tecnologías utilizadas
- **Lenguaje**: C#
- **Framework**: .NET Framework 4.5+
- **Base de Datos**: SQL Server
- **ORM**: Entity Framework
- **Patrón de Arquitectura**: Clean Architecture / Capas
- **Inyección de dependencias**

## Características
### Funcionalidades principales:
- Registrar libros con los siguientes datos:
  - Título
  - Año
  - Género
  - Número de páginas
  - Autor
- Registrar autores con los siguientes datos:
  - Nombre completo
  - Fecha de nacimiento
  - Ciudad de procedencia
  - Correo electrónico
- Validaciones de negocio:
  - Todos los datos marcados con asterisco (*) son obligatorios.
  - Se garantiza la integridad de la información.
  - Control del número de libros permitidos.
  - Si se supera el límite de libros, se retorna un error: `"No es posible registrar el libro, se alcanzó el máximo permitido."`
  - Si se intenta registrar un libro con un autor no existente, se retorna un error: `"El autor no está registrado."`

## Estructura del Proyecto
La solución está organizada en las siguientes carpetas:

```
Books Api
├── Application
│   ├── Dto (Definiciones de transferencia de datos)
│   ├── Exceptions (Manejo de errores)
│   ├── Interfaz (Interfaces de servicios)
│   ├── Response (Manejo de respuestas)
│   ├── Services (Implementación de lógica de negocio)
│   ├── Validations (Validaciones de datos)
│
├── Controllers (Controladores de la API)
│   ├── AuthorController.cs
│   ├── BookController.cs
│
├── Domain
│   ├── Entities (Modelos de base de datos)
│   ├── Interfaces (Interfaces para los repositorios)
│   ├── Migrations (Migraciones de la base de datos)
│
├── Infrastructure
│   ├── Data (Configuración de contexto de base de datos)
│   ├── DependencyInjection (Configuración de inyección de dependencias)
│   ├── Repository (Implementaciones de repositorios)
│
├── appsettings.json (Configuraciones de la aplicación)
├── Program.cs (Punto de entrada de la aplicación)
```

## Instalación y Ejecución
### Prerrequisitos
- **.NET Framework 4.5+**
- **SQL Server** instalado y configurado
- **Visual Studio** o cualquier IDE compatible con .NET

### Pasos para ejecutar
1. **Clonar el repositorio:**
   ```sh
   git clone https://github.com/tu-usuario/prueba-tecnica-net.git
   cd prueba-tecnica-net
   ```
2. **Configurar la base de datos:**
   - Modificar `appsettings.json` con la cadena de conexión a SQL Server.
3. **Ejecutar migraciones:**
   ```sh
   update-database
   ```
4. **Compilar y ejecutar:**
   ```sh
   dotnet run
   ```

## API Endpoints
| Método | Endpoint | Descripción |
|---------|----------|--------------|
| **POST** | `/api/authors` | Registrar un nuevo autor |
| **GET** | `/api/authors` | Obtener todos los autores |
| **POST** | `/api/books` | Registrar un nuevo libro |
| **GET** | `/api/books` | Obtener todos los libros |

## Contacto
Si tienes preguntas sobre este proyecto, contáctame en [tu correo o perfil de GitHub].


