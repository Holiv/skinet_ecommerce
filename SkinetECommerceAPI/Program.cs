using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using SkinetECommerceAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using SkinetECommerceAPI.Errors;
using SkinetECommerceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbcontext = services.GetService<StoreContext>();
var logger = services.GetService<ILogger<Program>>();
try
{
    await dbcontext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(dbcontext);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();

