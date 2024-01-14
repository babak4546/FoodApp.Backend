

using FoodApp.API.Security.DTOs;
using FoodApp.Core.Entities;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


SecurityServices.AddServices(builder);

var app = builder.Build();
SecurityServices.UseServices(app);


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
    var claims = new[]
    {
        new Claim("Type",result.Type.ToString()),
        new Claim("Username",result.Username.ToString()),
    };
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? ""));
    var signIn=new SigningCredentials (key,SecurityAlgorithms.HmacSha256);
var token = new JwtSecurityToken(

    builder.Configuration["Jwt:Issuer"],
    builder.Configuration["Jwt:Audience"],
    claims,
    expires: DateTime.UtcNow.AddDays(3),
    signingCredentials: signIn);
    
    
    return Results.Ok(new LoginResultDto
    {

        Message = "خوش امدید",
        IsOk = true,
        Token=new JwtSecurityTokenHandler().WriteToken(token),
    });
});
app.Run();

 