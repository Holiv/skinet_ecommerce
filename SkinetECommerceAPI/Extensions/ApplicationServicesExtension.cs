using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace SkinetECommerceAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
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
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse();
                    errorResponse.Errors = errors;

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}
