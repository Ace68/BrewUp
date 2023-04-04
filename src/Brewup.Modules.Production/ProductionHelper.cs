using Brewup.Modules.Production.Abstracts;
using Brewup.Modules.Production.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Modules.Production;

public static class ProductionHelper
{
    public static IServiceCollection AddProductionModule(this IServiceCollection services)
    {
        services.AddSingleton<IProductionService, ProductionService>();
        
        return services;
    }
}