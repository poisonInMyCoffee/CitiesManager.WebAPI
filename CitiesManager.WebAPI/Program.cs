using CitiesManager.WebAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options=>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Swagger
builder.Services.AddEndpointsApiExplorer();//Generates description for all end points
builder.Services.AddSwaggerGen(); //Generates OPenApi Specifications

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();

app.UseSwagger(); //Creates endpoint for swagger.json files 
app.UseSwaggerUI();//creates swagger UI for testing all web API endpoints/Action methods

app.UseAuthorization();

app.MapControllers();

app.Run();
