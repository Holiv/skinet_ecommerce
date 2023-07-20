using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinetECommerceAPI.Errors;

namespace SkinetECommerceAPI.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen();
            
            // ---> ADDING THE DBCONTEXT TO THE CONTAINER SERVICE
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            // ---> REGISTERING THE REPOSITORY IN THE SERVICES CONTAINER AS SCOPED, IT MEANS IT'LL HAVE A NEW INSTANCE OF IT FOR EACH REQUEST. IT WILL LIVE AND BE ACESSIBLE THROUGH THE METHODS TO BE EXECUTED DURING THE REQUEST.
            services.AddScoped<IProductRepository, ProductRepository>();

            // ---> WHEN ADDIND GENERICS TO THE CONTAINER, THE STRUCTURE MUST BE AS BELLOW
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<ApiBehaviorOptions>(options =>
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

            return services;
        }
    }
}
