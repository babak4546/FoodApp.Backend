

using FoodApp.API.Security.DTOs;
using FoodApp.Core.Entities;
using FoodApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FoodAppDB>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MainDB"));
});
builder.Services.AddCors(options
    =>options.AddDefaultPolicy(builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/signup",( FoodAppDB db ,ApplicationUser user)=>{
    db.ApplicationUsers.Add(user);
    db.SaveChanges();
    return Results.Ok();

});
app.MapPost("/signin", (FoodAppDB db, LoginDto login) =>
{
    var result = db.ApplicationUsers.FirstOrDefault(a => a.Username == login.Username && a.Password == login.Password);
    if (result == null)
    {
        return Results.Ok(new
        {
            Message = "نام کاربری یا کلمه عبور نادرست است",
            Success = false
        });
    }
    return Results.Ok(new
    {
        Message = "خوش امدید",
        Success = true
    });
});
app.Run();

 