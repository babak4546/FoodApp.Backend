using Ardalis.GuardClauses;
using Azure.Core;
using FoodApp.Api.Food.DTOs;
using FoodApp.Core.Entities;
using FoodApp.Core.Exceptions;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
SecurityServices.AddServices(builder);
var app = builder.Build();
SecurityServices.UseServices(app);
app.MapPost("/foods/{id}", (FoodAppDB db, ClaimsPrincipal user, int id) =>
{
    var username = user.Claims.FirstOrDefault(m => m.Type == "Username")?.Value ?? "";
    var foods = db
    .Foods
    .Include(m => m.Restaurant)
    .Where(m => m.Restaurant.Id == id && m.Restaurant.OwnerUsername == username)
    .Select(m => new FoodDto
    {

        Description = m.Description,
        Discount = m.Discount,
        Id = m.Id,
        IsAvailable = m.IsAvailable,
        Price = m.Price,
        Title = m.Title,


    })
    .ToList();
    return foods;

}).RequireAuthorization();
app.MapPost("/create", (FoodAppDB db, ClaimsPrincipal user, NewFoodDto newFood) =>
{
    var username = user.Claims.FirstOrDefault(m => m.Type == "Username")?.Value ?? "";
    var restaurant = db.Restaurants.FirstOrDefault(m => m.Id == newFood.RestaurantId);
    if (restaurant == null)
    {
        throw new RestaurantNotFoundException();
    }
    if (restaurant.OwnerUsername != username)
    {
        throw new ForbiddenRestaurantException();
    }
    var food = new Food();
    food.Restaurant = restaurant;
    food.IsAvailable = newFood.IsAvailable;
    food.Description = Guard.Against.NullOrEmpty(newFood.Description, message: "توضیحات باید وارد شود");
    food.Title = Guard.Against.NullOrEmpty(newFood.Title, message: "عنوان غذا اجباری است");
    food.Price = newFood.Price;
    food.Discount = newFood.Discount;
    db.Foods.Add(food);
    db.SaveChanges();
    return Results.Ok();
})
    .RequireAuthorization();

app.MapPost("/update", (FoodAppDB db, ClaimsPrincipal user, UpdateFoodDto ufood) =>
{
    var username = user.Claims.FirstOrDefault(m => m.Type == "Username")?.Value ?? "";
    var food = db.Foods.Include(m => m.Restaurant).FirstOrDefault(m => m.Id == ufood.Id);
    if (food == null)
    {
        throw new Exception("غذا یافت نشد");
    }
    if (food.Restaurant.OwnerUsername != username)
    {
        throw new ForbiddenRestaurantException();
    }

    food.IsAvailable = ufood.IsAvailable;
    food.Description = Guard.Against.NullOrEmpty(ufood.Description, message: "توضیحات باید وارد شود");
    food.Title = Guard.Against.NullOrEmpty(ufood.Title, message: "عنوان غذا اجباری است");
    food.Price = ufood.Price;
    food.Discount = ufood.Discount;
    db.SaveChanges();
    return Results.Ok();
})
    .RequireAuthorization();



app.MapPost("/remove/{id}", (FoodAppDB db, ClaimsPrincipal user, int id) =>
{
    var username = user.Claims.FirstOrDefault(m => m.Type == "Username")?.Value ?? "";
    var food = db.Foods.Include(m => m.Restaurant).FirstOrDefault(m => m.Id == id);
    if (food == null)
    {
        throw new Exception("غذا یافت نشد");
    }
    if (food.Restaurant.OwnerUsername != username)
    {
        throw new ForbiddenRestaurantException();
    }

    db.Foods.Remove(food);
    db.SaveChanges();
    return Results.Ok();
})
    .RequireAuthorization();
app.MapGet("showPhoto/{id}", (FoodAppDB db, int id) =>
{
    var food = db.Foods.FirstOrDefault(m => m.Id == id);
    if (food == null || food.Photo == null)
    {
        return Results.NotFound();
    }
    return Results.File(food.Photo, food.PhotoType);

});
app.MapPost("/upload/{id}", (FoodAppDB db, ClaimsPrincipal user, HttpContext http, int id) =>
{
    var file = http.Request.Form.Files[0];
    var folderName = Path.Combine("Resources", "Images");
    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
    if (file != null)
    {
        var food=db.Foods.FirstOrDefault(m => m.Id == id);
        if (food!=null)
        {
            using (var stream=new MemoryStream())
            {
                file.CopyTo(stream);
                food.Photo = stream.GetBuffer();    
                food.PhotoType=file.ContentType;
                db.SaveChanges();
            }
            return Results.Ok();
        }
        return Results.BadRequest();
        //var fileName = file.Name;
        //var fullPath = Path.Combine(pathToSave, fileName);
        //var dbPath = Path.Combine(folderName, fileName);
        //using (var stream = new FileStream(fullPath, FileMode.Create))
        //{
        //    file.CopyTo(stream);
        //}

    }
    else
    {
        return Results.BadRequest();
    }
}).RequireAuthorization();
app.Run();

//01:01:38