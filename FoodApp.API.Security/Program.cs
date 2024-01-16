

using FoodApp.API.Security.DTOs;
using FoodApp.Core.Entities;
using FoodApp.Core.Enums;
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


SecurityServices.AddServices(builder);

var app = builder.Build();
SecurityServices.UseServices(app);


app.MapPost("/signup", async( FoodAppDB db ,ApplicationUser user)=>{
    var rg = new Random();
    user.VerificationCode=rg.Next(100000,999999).ToString();
    //send sms 
   await db.ApplicationUsers.AddAsync(user);
   await db.SaveChangesAsync();
    return Results.Ok();

});
app.MapPost("/signin", async(FoodAppDB db, LoginDto login) =>
{
    if (!db.ApplicationUsers.Any())
    {
       await db.ApplicationUsers.AddAsync(new ApplicationUser
        {
            Email="admin@admin.com",
            Fullname="babak babaie",
            Username="admin",
            Password="admin",
            Type=ApplicationUserType.SystemAdmin,
            Verified=true

        });
        await db.SaveChangesAsync();
    }
    var result =await db.ApplicationUsers.FirstOrDefaultAsync(a => a.Verified==true && a.Username == login.Username && a.Password == login.Password);
    if (result == null)
    {
        return Results.Ok(new LoginResultDto
        {
            Message = "نام کاربری یا کلمه عبور اشتباه است",
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
        Type=result.Type.ToString(),
    });
});
app.Run();

 