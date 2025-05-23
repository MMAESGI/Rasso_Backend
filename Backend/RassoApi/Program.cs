using Microsoft.EntityFrameworkCore;
using RassoApi.Configuration;
using RassoApi.Database;
using RassoApi.Extensions;
using RassoApi.Services.DB;
using RassoApi.Services.Interfaces.DB;


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


var app = builder.Build();


// Application des migrations 
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
