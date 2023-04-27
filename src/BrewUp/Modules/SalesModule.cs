using Brewup.Modules.Sales;
using Brewup.Modules.Sales.Endpoints;

namespace Brewup.Modules;

public class SalesModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddSalesModule();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("v1/sales")
			.WithTags("Sales");

		mapGroup.MapPost("", SalesEndpoints.HandleAddOrder)
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("AddOrder");

		mapGroup.MapGet("", SalesEndpoints.HandleGetOrders)
			.Produces(StatusCodes.Status200OK)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("GetSalesOrders");
	}
}