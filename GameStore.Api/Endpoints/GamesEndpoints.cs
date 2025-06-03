
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;



public static class GamesEndpoints
{

    const String GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();


        // GET: /games
        group.MapGet("/", (GameStoreContext dbContext) =>
        {
            return Results.Ok(dbContext.Games.Include(g => g.Genre).ToList());
        });

        // GET: /games/{id}
        group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Include(g => g.Genre).FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(game);
        }).WithName(GetGameEndpointName);

        // POST: /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);

            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }).WithParameterValidation();

        // PUT: /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.Include(g => g.Genre).FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }

            game.Name = updatedGame.Name;
            game.Genre = dbContext.Genres.Find(updatedGame.GenreId);
            game.GenreId = updatedGame.GenreId;
            game.Price = updatedGame.Price;
            game.ReleaseDate = updatedGame.ReleaseDate;

            dbContext.SaveChanges();

            return Results.Ok(game);
        });


        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            Game? game = dbContext.Games.FirstOrDefault(g => g.Id == id);
            if (game is null)
            {
                return Results.NotFound();
            }

            dbContext.Games.Remove(game);
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        return group;
    }
}
