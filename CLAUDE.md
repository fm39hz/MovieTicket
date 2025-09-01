# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview
This is a MovieTicket web application - a movie ticket booking system built with ASP.NET Core and MariaDB.

## Architecture
The project follows Clean Architecture with these layers:
- **MovieTicket.Api**: Web API layer with controllers and startup configuration
- **MovieTicket.Application**: Application services and business logic
- **MovieTicket.Domain**: Domain entities, repositories, and business models
- **MovieTicket.Infrastructure**: Data access, external services, and infrastructure concerns

## Development Commands

### Build and Run
- Build the solution: `dotnet build`
- Run the API: `dotnet run --project src/MovieTicket.Api`
- Run with hot reload: `dotnet watch --project src/MovieTicket.Api`

### Database
- The application uses MariaDB/MySQL with Entity Framework Core
- Database is created automatically on first run in development
- Database context: `ApplicationDbContext` in MovieTicket.Infrastructure

### Testing
- No specific test framework configured yet
- Use `dotnet test` when tests are added

### Docker
- Docker compose configuration available in `compose.yaml`
- Run with: `docker compose up`
- API accessible on port 8080

## Key Features
- JWT-based authentication
- Swagger/OpenAPI documentation (available in development)
- Scalar API reference with DeepSpace theme
- Role-based authorization (RoleConstant defines roles)
- User management with admin capabilities

## API Documentation
- Swagger UI available at `/swagger` in development
- Scalar API reference available in development
- OpenAPI spec at `/openapi/v1.json`

## Project Structure Notes
- Controllers organized by feature: User, Admin, Authorization
- Domain entities include User, Movie with base model pattern
- Repository pattern implemented for data access
- Configuration through appsettings.json files
- Uses .NET 9 with nullable reference types enabled

## Important Files
- `src/MovieTicket.Api/Program.cs`: Main startup and configuration
- `src/MovieTicket.Domain/Constant/`: Application constants (routes, roles, task status)
- `compose.yaml`: Docker composition for development
- always check the build and run after complete a task to ensure there is atleast no syntax error