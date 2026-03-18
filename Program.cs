using CoreEFTest.DataContext;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using CoreEFTest.EFModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<CoreStudentsContext>(options => options
    .UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("CoreStudentsContext")));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<CoreEFTest.DataContext.CoreStudentsContext>();
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Creating DB...");
        context.Database.EnsureCreated();

        //stworzyc dwie grupy studenckie oraz pieciu studentow
        if(!context.Group.Any()) // sprawdza, czy sa jakies rekordy
        {
            var group1 = new Group { Name = "Grupa1" };
            var group2 = new Group { Name = "Grupa2" };

            context.Group.AddRange(group1, group2); // dodaje kilka encji jednoczesnie do tabeli??

            // grupa1
            var student1 = new Student { FirstName = "Borys", LastName = "Kowalski", Age = 20, Group = group1 };
            var student2 = new Student { FirstName = "Violetta", LastName = "Nowak", Age = 23, Group = group1 };

            // grupa2
            var student3 = new Student { FirstName = "Magdalena", LastName = "Zielinska", Age = 23, Group = group2 };
            var student4 = new Student { FirstName = "Oliwia", LastName = "Oliwna", Age = 20, Group = group2 };
            var student5 = new Student { FirstName = "Tomasz", LastName = "Wojcik", Age = 22, Group = group2 };

            context.Student.AddRange(student1, student2, student3, student4, student5);
            context.SaveChanges();
        }

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


app.MapRazorPages();


app.Run();
