using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.Core.Repository;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServiceExstention
    {
        public static IServiceCollection ServiceExtention(this IServiceCollection Services)
        {
           Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          
           Services.AddAutoMapper(typeof(MappingProfiles));
         
           Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var Errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(p => p.ErrorMessage)
                                                       .ToArray();
                    var ApiValidationResponError = new ApiVallidationErrorRespone(Errors);
                    return new BadRequestObjectResult(ApiValidationResponError);
                };
            });
            return Services;
        }
    }
}
