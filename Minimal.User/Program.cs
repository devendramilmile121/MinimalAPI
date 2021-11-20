using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal.User.MapperProfile;
using Minimal.User.Models.DTO;
using Minimal.User.Models.Entity;
using Minimal.User.Services;

var builder = WebApplication.CreateBuilder(args);

// Add db context
var connectionString = builder.Configuration.GetConnectionString("AppDb");
builder.Services.AddDbContext<UserDbContext>(o => o.UseSqlServer(connectionString));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add automapper
builder.Services.AddAutoMapper(typeof(UserProfile));

// Add user service in DI
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/api/GetAll",([FromServices] IUserService userService) =>
{
    try
    {
        var result = userService.GetAll();
        return Results.Ok(result);
    }
    catch (global::System.Exception e)
    {
        return Results.Problem(e.Message);
    }
}).WithName("GetAllUsers")
.Produces<List<GetAllUsersDTO>>(StatusCodes.Status200OK)
.Produces<string>(StatusCodes.Status500InternalServerError);

app.MapGet("/api/GetUserById/{id}", ([FromServices] IUserService userService, Guid id) =>
{
    try
    {
        var result = userService.GetById(id);
        if (result!= null)
        {
            return Results.Ok(result);
        }

        return Results.BadRequest();
    }
    catch (global::System.Exception e)
    {
        return Results.Problem(e.Message);
    }
}).WithName("GetUserById")
.Produces<GetUserDTO>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest)
.Produces<string>(StatusCodes.Status500InternalServerError);

app.MapPost("/api/CreateUser", ([FromServices] IUserService userService, [FromBody] CreateUserDTO user) =>
{
    try
    {
        var result = userService.Create(user);
        if (result != null)
        {
            return Results.Ok(result);
        }

        return Results.BadRequest("Something went wrong!");
    }
    catch (global::System.Exception e)
    {
        return Results.Problem(e.Message);
    }
}).WithName("CraeteUser")
.Produces<GetUserDTO>(StatusCodes.Status200OK)
.Produces<string>(StatusCodes.Status400BadRequest)
.Produces<string>(StatusCodes.Status500InternalServerError);

app.MapPut("/api/UpdateUser/{id}", ([FromServices] IUserService userService,Guid id, [FromBody] CreateUserDTO user) =>
{
    try
    {
        var result = userService.Update(id,user);
        if (result != null)
        {
            return Results.Ok(result);
        }

        return Results.BadRequest("Something went wrong!");
    }
    catch (global::System.Exception e)
    {
        return Results.Problem(e.Message);
    }
}).WithName("UpdateUser")
.Produces<GetUserDTO>(StatusCodes.Status200OK)
.Produces<string>(StatusCodes.Status400BadRequest)
.Produces<string>(StatusCodes.Status500InternalServerError);


app.MapDelete("/api/DeleteUser/{id}", ([FromServices] IUserService userService, Guid id) =>
{
    try
    {
        var result = userService.Delete(id);
        if (result)
        {
            return Results.Ok("User Deleted Successfully!");
        }

        return Results.BadRequest("Something went wrong!");
    }
    catch (global::System.Exception e)
    {
        return Results.Problem(e.Message);
    }
}).WithName("DeleteUser")
.Produces<string>(StatusCodes.Status200OK)
.Produces<string>(StatusCodes.Status400BadRequest)
.Produces<string>(StatusCodes.Status500InternalServerError);

app.Run();
