using Ambev.DeveloperEvaluation.Domain.Strategies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class WebApiModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            builder.Services.AddHealthChecks();
            builder.Services.Configure<List<DiscountRangeParametres>>(builder.Configuration.GetSection("DiscountRangeParametres"));
        }
    }
}
