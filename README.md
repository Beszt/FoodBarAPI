# FoodBarAPI
WEB API with info about food products (like energy, composition etc.) determined by EAN barcodes.

Live demo is available here [FoodBarAPI](http://foodbarapi.obisoft.pl).

## Technology stack

### Backend
- .NET 8.0 (C# 12)
- ASP.NET Minimal API
- JWT authentication
- Roles authorization
- MediatR
- FluentValidation
- NLog
- Swagger

### Database 
- Entity Framework
- AutoMapper
- PostgreSQL

### Tests 
- Xunit
- FluentAssertion

### Patterns
- Clean Architecture
- CQRS

### DevOps
- CI/CD pipelines
- Branch protection rules
- Docker support

## Quick Setup
1. Install [PostgreSQL Server](https://www.postgresql.org/download/) and make new database.
2. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
3. Download source code (all projects)
4. Navigate to `FoodBarAPI.Presentation` project folder and open `appsettings.json`.

    4.1. Set connection string to your PostgreSQL database instance. In example:
    ```
    "Host=localhost;Database=DATABASE;Username=USERNAME;Password=PASSWORD;"
    ```
    4.2. Set JWT auhtentication config. In example:
    ```
    "Key": "YOUR VERY HIDDEN SERCET PHARSE",",
    "ExpireInDays": 14,
    "Issuer": "https://yourhost.com"
    ```

5. Change `nlog.config.example` to `nlog.config`. Optionally, if you want logging with email just replace smtp credentuals with yours and uncomment email logger.
6. Type `dotnet run`

## Quick Setup (Docker)
1. Install [PostgreSQL Server](https://www.postgresql.org/download/) and make new database.
2. Pull [Docker image](https://hub.docker.com/r/beszt/foodbarapi)
3. Run image with the following enviroment variables and change it values. In example:

```
ConnectionStrings__FoodBarAPI: Host=localhost;Database=DATABASE;Username=USERNAME;Password=PASSWORD;
Jwt__Key: YOUR VERY HIDDEN SERCET PHARSE
Jwt__ExpireInDays: 14
Jwt__Issuer: https://yourhost.com
```
