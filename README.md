# Game Store API

A minimal RESTful API for managing a game store, built with ASP.NET Core and Entity Framework Core (using SQLite).

## Features

- CRUD operations for Games
- Genre management (seeded with default genres)
- Entity Framework Core with SQLite
- Minimal API endpoints
- DTO mapping for clean API responses

## Getting Started

### Prerequisites

- [.NET 8 or 9 SDK](https://dotnet.microsoft.com/download)
- [SQLite](https://www.sqlite.org/download.html) (optional, for DB browsing)
- (Optional) [DB Browser for SQLite](https://sqlitebrowser.org/) for viewing the database

### Setup

1. **Clone the repository:**

   ```bash
   git clone https://github.com/swissjake/Game-api.git
   cd game-store
   ```

2. **Restore dependencies:**

   ```bash
   dotnet restore
   ```

3. **Run database migrations and start the API:**

   ```bash
   dotnet run --project GameStore.Api
   ```

   The database (`GameStore.db`) will be created automatically in the `GameStore.Api` folder.

4. **Test the API:**
   - Use tools like [Postman](https://www.postman.com/) or VS Code REST Client.
   - Example endpoints:
     - `GET /games`
     - `POST /games`
     - `PUT /games/{id}`
     - `DELETE /games/{id}`

## Project Structure

```
GameStore.Api/
  ├── Data/         # EF Core DbContext and migrations
  ├── Dtos/         # Data Transfer Objects
  ├── Endpoints/    # Minimal API endpoint definitions
  ├── Entities/     # Database entities
  ├── Mapping/      # Mapping extensions between entities and DTOs
  ├── Program.cs    # Main entry point
  ├── appsettings.json
  └── GameStore.db  # SQLite database (auto-generated)
```

## Example HTTP Request

```http
POST /games
Content-Type: application/json

{
  "name": "Call of Duty: Modern Warfare",
  "genreId": 4,
  "price": 9.99,
  "releaseDate": "2023-10-01"
}
```

## Notes

- Default genres are seeded: Fighting, Adventure, Racing, Shooter, Strategy.
