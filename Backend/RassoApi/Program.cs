using RassoApi.Configuration;
using RassoApi.Database;
using RassoApi.Extensions;
using RassoApi.Services.Events.Interfaces;
using RassoApi.Services.Events;
using static Common.CommonExtension;
using Microsoft.OpenApi.Models;
using Identity.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rasso API", Version = "v1" });

});

// Extension pour l'injection de dépendance
builder.Services.AddAppSettingsConfiguration(builder.Configuration);
builder.Services.AddApplicationServices();

// add authorisation pour la policy
builder.Services.AddCustomAuthentification(builder.Configuration);


// Database 
builder.Services.AddDataBaseServices();

// Utilisation du package commun
builder.Services.AddCommonServices<AppDbContext>();

builder.Services.AddHttpClient("Identity", client =>
{
    client.BaseAddress = new Uri("http://identity:8080");
});
builder.Services.AddScoped<IdentityClient>(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    var httpClient = factory.CreateClient("Identity");
    var baseUrl = httpClient.BaseAddress!.ToString();

    return new IdentityClient(baseUrl, httpClient);
});

builder.Services.AddScoped<IUserProxyService, UserProxyService>();

builder.Services.AddCorsConfiguration();

var app = builder.Build();

app.UseCors("AllowAll");

// Utilisation du package commun
app.UseCommonPackage<AppDbContext>();


app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
