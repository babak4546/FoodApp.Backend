

using FoodApp.API.Security.DTOs;
using FoodApp.Core.Entities;
using FoodApp.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
    =>options.AddDefaultPolicy(builder=>builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/signup", async( FoodAppDB db ,ApplicationUser user)=>{
   await db.ApplicationUsers.AddAsync(user);
   await db.SaveChangesAsync();
    return Results.Ok();

});
app.MapPost("/signin", async(FoodAppDB db, LoginDto login) =>
{
    var result =await db.ApplicationUsers.FirstOrDefaultAsync(a => a.Username == login.Username && a.Password == login.Password);
    if (result == null)
    {
        return Results.Ok(new LoginResultDto
        {
            Message = "نام کاربری یا کلمه عبور نادرست است",
            IsOk = false
        });
    }
    return Results.Ok(new LoginResultDto
    {
        Message = "خوش امدید",
        IsOk = true
    });
});
app.Run();

 