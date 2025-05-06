using Ambev.DeveloperEvaluation.Domain.Strategys;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class DomainModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IDiscountStrategy, QuantityRangeDiscountStrategy>();
    }
}