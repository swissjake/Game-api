
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
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
            return Results.Ok(dbContext.Games.Include(g => g.Genre).Select(g => g.ToGameSummaryDto()).AsNoTracking());
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
            Game game = newGame.ToEntity();
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameSummaryDto());
        });

        // PUT: /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = dbContext.Games.Include(g => g.Genre).FirstOrDefault(g => g.Id == id);
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            dbContext.SaveChanges();

            return Results.NoContent();
        });


        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            dbContext.Games.Where(g => g.Id == id).ExecuteDelete();
            return Results.NoContent();
        });

        return group;
    }
}
