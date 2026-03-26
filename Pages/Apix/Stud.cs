using Microsoft.EntityFrameworkCore;
using CoreEFTest.DataContext;
using CoreEFTest.EFModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace Lab1.Pages.Apix;

public static class Stud
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (CoreStudentsContext db) =>
        {
            return await db.Student.ToListAsync();
        })
        .WithName("GetAllStudents")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Student>, NotFound>> (int studentid, CoreStudentsContext db) =>
        {
            return await db.Student.AsNoTracking()
                .FirstOrDefaultAsync(model => model.StudentId == studentid)
                is Student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int studentid, Student student, CoreStudentsContext db) =>
        {
            var affected = await db.Student
                .Where(model => model.StudentId == studentid)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.StudentId, student.StudentId)
                    .SetProperty(m => m.LastName, student.LastName)
                    .SetProperty(m => m.FirstName, student.FirstName)
                    .SetProperty(m => m.Age, student.Age)
                    .SetProperty(m => m.GroupId, student.GroupId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateStudent")
        .WithOpenApi();

        group.MapPost("/", async (Student student, CoreStudentsContext db) =>
        {
            db.Student.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Student/{student.StudentId}",student);
        })
        .WithName("CreateStudent")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int studentid, CoreStudentsContext db) =>
        {
            var affected = await db.Student
                .Where(model => model.StudentId == studentid)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .WithOpenApi();
    }
}
