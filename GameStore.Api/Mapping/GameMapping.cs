

using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping;

public static class GameMapping
{

    // CreateGameDto
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    // GameSummaryDto
    public static GameSummaryDto ToGameSummaryDto(this Game game)
    {
        return new(game.Id, game.Name, game.Genre?.Name ?? "Unknown", game.Price, game.ReleaseDate);
    }

    // GameDetailsDto
    public static GameDetailsDto ToGameDetailsDto(this Game game)
    {
        return new(game.Id, game.Name, game.GenreId, game.Price, game.ReleaseDate);
    }


    // UpdateGameDto
    public static Game ToEntity(this UpdateGameDto game, int id)
    {
        return new Game()
        {
            Id = id,
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }
}


