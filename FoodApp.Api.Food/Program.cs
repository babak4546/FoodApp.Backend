using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
SecurityServices.AddServices(builder);
var app = builder.Build();
SecurityServices.UseServices(app);
app.MapPost("/foods/{id}",(FoodAppDB db ,ClaimsPrincipal user ,int id) =>
{
    var username = user.Claims.FirstOrDefault(m => m.Type == "Username")?.Value ?? "";
    var foods = db.Foods.Include(m=>m.Restaurant).Where(m => m.Restaurant.Id == id && m.Restaurant.OwnerUsername == username).ToList();
    return foods;

}).RequireAuthorization();
app.Run();

