using RassoApi.Configuration;
using RassoApi.Database;
using RassoApi.Extensions;
using RassoApi.Services.Events.Interfaces;
using RassoApi.Services.Events;
using static Common.CommonExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extension pour l'injection de dépendance
builder.Services.AddAppSettingsConfiguration(builder.Configuration);
builder.Services.AddApplicationServices();

// add authorisation pour la policy
builder.Services.AddCustomAuthentification(builder.Configuration);


// Database 
builder.Services.AddDataBaseServices();

// Utilisation du package commun
builder.Services.AddCommonServices<AppDbContext>();

builder.Services.AddHttpClient<IUserProxyService, UserProxyService>(client =>
{
    // Url du microservice Identity
    client.BaseAddress = new Uri("http://identity:8080");
});

var app = builder.Build();

// Utilisation du package commun
app.UseCommonPackage<AppDbContext>();


app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
