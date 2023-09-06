using UscastoresProject.Data;
using UscastoresProject.Service;
using UscastoresProject.Models;
using UscastoresProject.controller;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<UscastoresRepository>();
builder.Services.AddTransient<IUscastores, UscastoresService>();
builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


var uscastoresController = new UscastoresController();
uscastoresController.MapRoutes(app); // Map routes using the controller

app.Run();
