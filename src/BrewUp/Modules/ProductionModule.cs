using Brewup.Modules.Production;
using Brewup.Modules.Production.Endpoints;

namespace Brewup.Modules;

public class ProductionModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    
    public void RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddProductionModule();
    }

    public void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var mapGroup = endpoints.MapGroup("v1/production")
            .WithTags("Production");

        mapGroup.MapGet("", ProductionEndpoints.HandleGetProductionOrders)
            .WithName("GetProductionOrders");
    }
}