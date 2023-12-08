using Microsoft.EntityFrameworkCore;
using TravelAPI.DBContexts;
using TravelAPI.Models;
using TravelAPI.Services;
using TravelAPI.Controllers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Add Swagger services
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemoryDatabase"); // Provide a unique name for your in-memory database
});

// Services
builder.Services.AddScoped<ActivityTypeService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<BudgetService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<DestinationService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VoyageService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger-UI (HTML, JS, CSS, etc.),
    // specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelAPI"); // Adjust the endpoint path if necessary
    });
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
