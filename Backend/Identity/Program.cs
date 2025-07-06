using System.Configuration;
using Identity.Database;
using Identity.Extensions;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using static Common.CommonExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity API", Version = "v1" });

});

// Extension pour l'injection de dépendance
builder.Services.AddApplicationServices();

// Utilisation du package commun
builder.Services.AddCommonServices<AppDbContext>();

builder.Services.AddIdentityServices<AppDbContext, User, Role>();



var app = builder.Build();

// Utilisation du package commun
app.UseCommonPackage<AppDbContext>();

// Peuplement des données
if (!args.Contains("swagger"))
{
    await app.SeedIdentityDataAsync();
}



app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
