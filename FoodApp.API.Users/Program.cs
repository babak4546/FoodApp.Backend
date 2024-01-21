using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Security;
using FoodApp.Infrastructure.UI;

var builder = WebApplication.CreateBuilder(args);
SecurityServices.AddServices(builder);
var app = builder.Build();
SecurityServices.UseServices(app);

app.MapPost("/list",async (FoodAppDB db) =>
{

    return Results.Ok(db.ApplicationUsers
        .ToList());
        
     
});
app.MapPost("/alist", async (FoodAppDB db,ListRequestDTO listRequest) =>
{
    return Results.Ok(db.ApplicationUsers.ToList());
});

app.Run();

