using FoodApp.API.Home.DTOs;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
SecurityServices.AddServices(builder);
var app = builder.Build();
SecurityServices.UseServices(app);

app.MapGet("/home", (FoodAppDB db) =>
{
    return db
    .Foods
    .Include(m => m.Restaurant)
    .Where(m=>m.IsAvailable==true && m.Restaurant.IsApproved && m.Restaurant.IsActive)
    .Select(m=> new FoodDto
    {
        Description = m.Description,
        Discount = m.Discount,
        Id = m.Id,
        Price = m.Price,
        Title = m.Title,
        RestaurantTitle=m.Restaurant.Title
    })
    .ToList();
});
app.Run();

 