using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;



public static class GamesEndpoints
{

    const String GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99m, new DateOnly(2017, 3, 3)),
    new GameDto(2, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27)),
    new GameDto(3, "Animal Crossing: New Horizons", "Simulation", 59.99m, new DateOnly(2020, 3, 20)),
    new GameDto(4, "Metroid Dread", "Action-Adventure", 59.99m, new DateOnly(2021, 10, 8)),
    new GameDto(5, "Splatoon 2", "Shooter", 59.99m, new DateOnly(2017, 7, 21))
    ];


    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();

       
// GET: /games
        group.MapGet("/", () => games);

// GET: /games/{id}
group.MapGet("/{id}", (int id) =>
{
    GameDto? game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName(GetGameEndpointName);

// POST: /games
group.MapPost("/", (CreateGameDto newGame) =>
{
    GameDto game = new(
         games.Count + 1,
         newGame.Name,
         newGame.Genre, 
         newGame.Price,
         newGame.ReleaseDate
     );
    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
}).WithParameterValidation();

// PUT: /games/{id}
group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    game = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    games[games.FindIndex(g => g.Id == id)] = game;
    return Results.Ok(game);
});


group.MapDelete("/{id}", (int id) =>
{
    var game = games.Find(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }

    games.Remove(game);
    return Results.NoContent();
});

        return group;
    }
}
