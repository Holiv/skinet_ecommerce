using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using SkinetECommerceAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---> ADDING THE DBCONTEXT TO THE CONTAINER SERVICE
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ---> REGISTERING THE REPOSITORY IN THE SERVICES CONTAINER AS SCOPED, IT MEANS IT'LL HAVE A NEW INSTANCE OF IT FOR EACH REQUEST. IT WILL LIVE AND BE ACESSIBLE THROUGH THE METHODS TO BE EXECUTED DURING THE REQUEST.
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ---> WHEN ADDIND GENERICS TO THE CONTAINER, THE STRUCTURE MUST BE AS BELLOW
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

