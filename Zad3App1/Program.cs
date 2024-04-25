using Microsoft.EntityFrameworkCore;
using Zad3App1.Data;
using Zad3App1.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AnimalContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("AnimalContext")));

var app = builder.Build();

// Initialize and seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AnimalContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated(); 

    
    context.Animals.AddRange(
        new Animal { Name = "Lion", Description = "A large wild cat", Category = "Mammals", Area = "Savannah" },
        new Animal { Name = "Eagle", Description = "A bird of prey", Category = "Birds", Area = "Mountains" }
        
    );
    context.SaveChanges();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();