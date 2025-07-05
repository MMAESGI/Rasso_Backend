using Identity.Database;
using Identity.Extensions;
using Identity.Models;
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
builder.Services.AddIdentityServices<AppDbContext, User, Role>();


var app = builder.Build();

// Utilisation du package commun
app.UseCommonPackage<AppDbContext>();

// Peuplement des données
await app.SeedIdentityDataAsync();


app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
