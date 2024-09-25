using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//My extension method,where i have putted every services
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

//create db while starting project,it uses all migrations or just the latest
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context=services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    var logger=services.GetRequiredService<ILogger<Program>>();//say which class would be logger logs
    logger.LogError(ex,"An error occured while migration");
}

app.Run();
