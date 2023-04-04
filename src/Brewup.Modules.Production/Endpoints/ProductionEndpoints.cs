using Brewup.Modules.Production.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Production.Endpoints;

public static class ProductionEndpoints
{
    public static async Task<IResult> HandleGetProductionOrders(IProductionService productionService)
    {
        return Results.Ok();
    }
}