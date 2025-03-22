# 📚 Books API - Prueba Técnica  

## 📌 Descripción  
Este proyecto es una API desarrollada en **.NET 8** con **C#** y **SQL Server** como base de datos. Su función principal es gestionar libros y autores, permitiendo su registro y aplicando reglas de negocio.  

## 🚀 Tecnologías utilizadas  
- **Lenguaje**: C#  
- **Framework**: ASP.NET Web API  
- **Base de Datos**: SQL Server  
- **ORM**: Entity Framework Core  
- **Patrón de Arquitectura**: Clean Architecture (Capas)  
- **Inyección de dependencias**  

## ✨ Características  
### Funcionalidades principales:  
- **Registro de libros** con los siguientes datos:  
  - Título  
  - Año  
  - Género  
  - Número de páginas  
  - Autor  
- **Registro de autores** con los siguientes datos:  
  - Nombre completo  
  - Fecha de nacimiento  
  - Ciudad de procedencia  
  - Correo electrónico  

### 🛑 Validaciones de negocio  
- Todos los datos obligatorios están marcados con (*).  
- Se garantiza la integridad de la información.  
- **Límite de libros por autor**:  
  - Si se alcanza el máximo permitido, se retorna un error:  
    ```json
    { "error": "No es posible registrar el libro, se alcanzó el máximo permitido." }
    ```
  - Si se intenta registrar un libro con un autor no existente, se retorna un error:  
    ```json
    { "error": "El autor no está registrado." }
    ```  

## 🏗️ Estructura del Proyecto  

```
Books API
├── Application
│   ├── Dto (Transferencia de datos)
│   ├── Exceptions (Manejo de errores)
│   ├── Interfaces (Interfaces de servicios)
│   ├── Response (Manejo de respuestas)
│   ├── Services (Lógica de negocio)
│   ├── Validations (Validaciones)
│
├── Controllers (Controladores de la API)
│   ├── AuthorController.cs
│   ├── BookController.cs
│
├── Domain
│   ├── Entities (Modelos de base de datos)
│   ├── Interfaces (Interfaces de repositorios)
│   ├── Migrations (Migraciones)
│
├── Infrastructure
│   ├── Data (Configuración del contexto de DB)
│   ├── DependencyInjection (Configuración de DI)
│   ├── Repository (Repositorios)
│
├── appsettings.json (Configuraciones)
├── Program.cs (Punto de entrada)
```

---

## 📦 Instalación y Ejecución  

### ⚠️ Prerrequisitos  
- **.NET 8 SDK**  
- **SQL Server** instalado y configurado  
- **Visual Studio** o **VS Code** con extensión de C#  
- **Entity Framework Core** instalado  

### 🛠️ Instalación  

1️⃣ **Clonar el repositorio:**  
```sh
git clone https://github.com/tu-usuario/prueba-tecnica-net.git
cd prueba-tecnica-net
```

2️⃣ **Instalar herramientas globales necesarias:**  
Si no tienes instalado `dotnet ef`, ejecuta:  
```sh
dotnet tool install --global dotnet-ef
```
> **Nota:** Si ya lo tienes instalado y quieres actualizarlo, usa:  
```sh
dotnet tool update --global dotnet-ef
```

3️⃣ **Configurar la base de datos:**  
Modifica `appsettings.json` con tu conexión a SQL Server.  

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Conexion": "Server=localhost,1433;Database=Books;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;TrustServerCertificate=True;"
  }
}
```

> **Nota:** Reemplaza `TU_USUARIO` y `TU_CONTRASEÑA` con las credenciales correctas.  

4️⃣ **Ejecutar migraciones:**  
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

5️⃣ **Ejecutar el proyecto:**  
```sh
dotnet run
```

---

## 🗄️ Script de Base de Datos  

```sql
CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    DateOfBirth DATE NOT NULL,
    CityOfOrigin NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE NOT NULL
);

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Year INT NOT NULL,
    Genre NVARCHAR(100),
    PageCount INT NOT NULL,
    AuthorId INT NOT NULL,
    CONSTRAINT FK_Books_Author FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);
```

---

## 📌 API Endpoints  

### **AuthorController**  
| Método | Ruta              | Descripción |
|--------|-------------------|-------------|
| `GET`  | `/api/author`     | Obtener todos los autores (paginado). |
| `GET`  | `/api/author/{id}` | Obtener un autor por ID. |
| `POST` | `/api/author`     | Agregar un nuevo autor. |
| `PUT`  | `/api/author/{id}` | Actualizar un autor por ID. |
| `DELETE` | `/api/author/{id}` | Eliminar un autor. |

### **BookController**  
| Método | Ruta              | Descripción |
|--------|-------------------|-------------|
| `GET`  | `/api/book/{id}`  | Obtener un libro por ID. |
| `POST` | `/api/book`       | Agregar un nuevo libro. |
| `PUT`  | `/api/book/{id}`  | Actualizar un libro por ID. |
| `DELETE` | `/api/book/{id}` | Eliminar un libro. |

---
## 📞 Contacto  
Si tienes preguntas o sugerencias sobre este proyecto, puedes contactarme a través de:  

- **GitHub**: [kevin-villegas13](https://github.com/kevin-villegas13)  
- **Correo electrónico**: [kevinvilleperez@gmail.com](mailto:kevinvilleperez@gmail.com)  
- **WhatsApp**: [Envíame un mensaje](https://wa.me/573173552802)  

