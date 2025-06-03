using System;
using GameStore.Api.Data;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{

    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET: /genres
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var genres = await dbContext.Genres.Select(g => g.ToGenreDto()).AsNoTracking().ToListAsync();
            return Results.Ok(genres);
        });

        // GET: /genres/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var genre = await dbContext.Genres.FindAsync(id);
            if (genre is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(genre.ToGenreDto());
        });

        return group;
    }




}
