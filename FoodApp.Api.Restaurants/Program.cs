using FoodApp.Core.Entities;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

SecurityServices.AddServices(builder);
var app = builder.Build();
SecurityServices.UseServices(app);

app.MapPost("/requestLicst", async (FoodAppDB db) =>
{

    return Results.Ok(
     db.Restaurants
    .Where(r => r.IsApproved == false)
    .ToList());
        
});
app.MapGet("/requestcount", async (FoodAppDB db) =>
{
return Results.Ok(
    new
    {
      Count=await db.Restaurants.CountAsync(c=>c.IsApproved==false)
    });
});
 app.Run();

