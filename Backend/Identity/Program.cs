using Identity;
using Identity.Database;
using Microsoft.EntityFrameworkCore;
using static Common.CommonExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Extension pour l'injection de dépendance
builder.Services.AddApplicationServices();

// Utilisation du package commun
builder.Services.AddCommonServices<AppDbContext>();

var app = builder.Build();

// Utilisation du package commun
app.UseCommonPackage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
