

# 📚 Library Management System (LMS)

This project is a **Library Management System (LMS)** built using **Clean Architecture** with **.NET 8.0**. It follows best practices by separating concerns into different layers.

## 🏗️ Architecture

The project is divided into **three main layers**:

1. **LMS.API** (Presentation Layer)  
   - Contains API controllers that expose endpoints.
   - Handles HTTP requests and responses.
   - Uses dependency injection to call the application layer services.

2. **LMS.Core** (Application Layer)  
   - Contains **DTOs**, **Services**, and **Interfaces**.
   - Business logic is implemented in this layer.
   - Includes validation for request DTOs.

3. **LMS.Infrastructure** (Data Access Layer)  
   - Contains **Repositories** for database operations.
   - Uses **Entity Framework Core** for database management.
   - Includes migrations and seed data.

## 📂 Project Structure

```
LMS
│── LMS.API (Presentation Layer)
│   ├── Controllers
│   │   ├── BooksController.cs
│   │   ├── LoansController.cs
│   │   ├── UsersController.cs
│   │   ├── WeatherForecastController.cs
│   ├── CustomActionFilter
│   ├── Middleware (Exception Handling)
│   ├── Program.cs
│
│── LMS.Core (Application Layer)
│   ├── DTOs
│   │   ├── RequestDTOs
│   │   │   ├── BookAddRequest.cs
│   │   │   ├── LoanAddRequest.cs
│   │   │   ├── UserAddRequest.cs
│   │   ├── ResponseDTOs
│   │   │   ├── BookResponse.cs
│   │   │   ├── LoanResponse.cs
│   │   │   ├── UserResponse.cs
│   ├── Interfaces
│   │   ├── Repositories
│   │   │   ├── IBookRepository.cs
│   │   │   ├── ILoanRepository.cs
│   │   │   ├── IUserRepository.cs
│   │   ├── Services
│   │   │   ├── IBooksService.cs
│   │   │   ├── ILoansService.cs
│   │   │   ├── INotificationService.cs
│   │   │   ├── IUsersService.cs
│
│── LMS.Infrastructure (Data Access Layer)
│   ├── Data
│   │   ├── ApplicationDbContext.cs
│   ├── Mappings
│   │   ├── MappingProfile.cs
│   ├── Middlewares
│   │   ├── ExceptionHandlingMiddleware.cs
│   ├── Migrations
│   ├── Repositories
│   │   ├── BooksRepository.cs
│   │   ├── LoansRepository.cs
│   │   ├── UsersRepository.cs
│   ├── Services
│   │   ├── BookService.cs
│   │   ├── LoansService.cs
│   │   ├── UserService.cs
```

## 🚀 Features

✅ **Book Management** (Add, Update, Delete, View Books)  
✅ **User Management** (Register, Update, Delete Users)  
✅ **Loan System** (Borrow and Return Books)  
✅ **Exception Handling Middleware**  
✅ **Validation for DTOs using FluentValidation**  
✅ **Dependency Injection for Services and Repositories**  

## 🔧 Technologies Used

- **ASP.NET Core 8.0** - Web API framework
- **Entity Framework Core** - ORM for database management
- **FluentValidation** - For request validation
- **AutoMapper** - For mapping DTOs and Entities
- **SQL Server** - Database
- **Dependency Injection** - For better maintainability
- **Clean Architecture** - Follows separation of concerns

## 📌 API Endpoints

### 📚 Books API

| Method | Endpoint          | Description            |
|--------|------------------|------------------------|
| `GET`  | `/api/Books`      | Get all books          |
| `POST` | `/api/Books`      | Add a new book         |
| `GET`  | `/api/Books/{id}` | Get book by ID         |
| `PUT`  | `/api/Books/{id}` | Update book by ID      |
| `DELETE` | `/api/Books/{id}` | Delete book by ID      |

### 👤 Users API

| Method | Endpoint          | Description            |
|--------|------------------|------------------------|
| `GET`  | `/api/Users`      | Get all users         |
| `POST` | `/api/Users`      | Register new user      |
| `GET`  | `/api/Users/{id}` | Get user by ID        |
| `PUT`  | `/api/Users/{id}` | Update user by ID     |
| `DELETE` | `/api/Users/{id}` | Delete user by ID     |

### 📖 Loans API

| Method | Endpoint          | Description            |
|--------|------------------|------------------------|
| `GET`  | `/api/Loans`      | Get all loan records  |
| `POST` | `/api/Loans`      | Issue a new loan      |
| `PUT`  | `/api/Loans/{id}/return` | Return a borrowed book |


## ⚡ How to Run

1. **Clone the Repository**  
   ```sh
   git clone https://github.com/MahmoodElbadri/LMS-with-CleanArchitecture.git
   cd LMS-with-CleanArchitecture
   ```

2. **Setup Database**  
   - Update **appsettings.json** with your **SQL Server** connection string.
   - Run EF migrations:
     ```sh
     dotnet ef database update
     ```

3. **Run the API**  
   ```sh
   dotnet run --project LMS.API
   ```

4. **Access API**  
   Open browser and navigate to:  
   - **Swagger UI:** `http://localhost:5000/swagger`
   - **API Endpoints:** `http://localhost:5000/api`

## 👨‍💻 Contributors

- [**Mahmoud Salah Elbadri**](https://github.com/MahmoodElbadri) 👨‍💻

---

This **README.md** provides an overview of your project, its structure, endpoints, and setup instructions. Let me know if you want to modify anything before adding it to GitHub! 🚀
