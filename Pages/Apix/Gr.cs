using Microsoft.EntityFrameworkCore;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace Lab1.Pages.Apix;

public static class Gr
{
    public static void MapGroupEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Group").WithTags(nameof(Group));

        group.MapGet("/", async (CoreStudentsContext db) =>
        {
            return await db.Group.ToListAsync();
        })
        .WithName("GetAllGroups")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Group>, NotFound>> (int groupid, CoreStudentsContext db) =>
        {
            return await db.Group.AsNoTracking()
                .FirstOrDefaultAsync(model => model.GroupId == groupid)
                is Group model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetGroupById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int groupid, Group @group, CoreStudentsContext db) =>
        {
            var affected = await db.Group
                .Where(model => model.GroupId == groupid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.GroupId, @group.GroupId)
                    .SetProperty(m => m.Name, @group.Name)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateGroup")
        .WithOpenApi();

        group.MapPost("/", async (Group @group, CoreStudentsContext db) =>
        {
            db.Group.Add(@group);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Group/{@group.GroupId}",@group);
        })
        .WithName("CreateGroup")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int groupid, CoreStudentsContext db) =>
        {
            var affected = await db.Group
                .Where(model => model.GroupId == groupid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteGroup")
        .WithOpenApi();
    }
}
