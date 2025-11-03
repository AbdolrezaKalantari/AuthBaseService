# 🔐 AuthBaseService

AuthBaseService is a clean, modular authentication system built with ASP.NET Core and SQL Server. It supports user registration, login, email confirmation, JWT token generation, and refresh token management — all structured with a 5-layer architecture.

---

## 📦 Project Structure
- `Domain` – Core models and interfaces  
- `Data` – EF Core context and repositories  
- `Application` – Services, DTOs, validation logic
- `IoC` – Dependency injection setup 
- `API` – RESTful controllers and entry point

---

## 🚀 Features

- User registration with email confirmation
- Login with JWT token issuance
- Email confirmation via secure token
- Refresh token generation and renewal
- Input validation using FluentValidation
- RESTful error handling with meaningful responses

---
## 🔭 Future Improvements

- Password reset and change
- Role-based authorization
- Swagger documentation enhancements
- Unit and integration testing
- Docker support for deployment

---

## 🛠 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Swagger or Postman for API testing

---

## ⚙️ Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/AuthBaseService.git
   ```
## Configure your connection string and app settings in appsettings.json:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=AuthDb;Trusted_Connection=True;Encrypt=False;"
  },
  "Jwt": {
    "Key": "your JWT Key",
    "Issuer": "AuthApp",
    "Audience": "AuthAppUsers"
  },
  "Mail": {
    "From": "kalantari@sejongwords.ir",
    "SmtpHost": "sejongwords.ir",
    "SmtpPort": "465",
    "Username": "your Username",
    "Password": "your password"
  },
  "App": {
    "BaseUrl": "https://localhost:7285" // change this to your deployed url
  }
}
```
## Apply migrations and create the database:
```bash
dotnet ef database update
```
## Run the project:
```bash
dotnet run --project AuthBaseService.API
```
## Access Swagger UI:
```bash
https://localhost:7285/swagger
```
---
## 📧Email Testing
To test email confirmation, make sure your SMTP settings are correct. You can use services like Mailtrap or Ethereal Email for development.
## 📌 License
This project is licensed under the MIT License.

## 🙌 Author
**Developed by** Developed by Abdolreza Kalantari
**Contact:** Contact: Abdolrezakalantari2024@gmail.com 
