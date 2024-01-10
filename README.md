# FoodBarAPI
WEB API with info about food products (like energy, composition etc.) determined by EAN barcodes.

## Technology stack

### Backend
- NET 8.0 (C# 12)
- ASP.NET Minimal API
- JWT authentication
- Roles authorization
- MediatR
- FluentValidation
- NLog
- Swagger

## Database 
- Entity Framework
- AutoMapper
- PostgreSQL

## Tests 
- Xunit
- FluentAssertion

### Patterns
- Clean Architecture
- CQRS (Mediator)

## Quick Setup
1. Install [PostgreSQL Server](https://www.postgresql.org/download/) and make new database.
2. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
3. Download source code (all projects)
4. Navigate to `FoodBarAPI.Presentation` project folder and open `appsettings.json`.

    4.1. Set connection string to your PostgreSQL database instance. In example:
    ```
    "Host=localhost;Database=CarWorkshop;Username=postgresql;Password=PASSWORD;"
    ```
    4.2. Set JWT auhtentication config. In example:
    ```
    "Key": "YOUR VERY HIDDEN SERCET PHARSE",",
    "ExpireInDays": 14,
    "Issuer": "http://localhost:5569"
    ```

5. Change `nlog.config.example` to `nlog.config`. Optionally, if you want logging with email just replace smtp credentuals with yours and uncomment email logger.
6. Type `dotnet run`