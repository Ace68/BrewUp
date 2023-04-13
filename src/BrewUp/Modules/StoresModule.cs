using Brewup.Modules.Warehouse;
using Brewup.Modules.Warehouse.Endpoint;

namespace Brewup.Modules;

public class WarehouseModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddWarehouseModule();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("v1/warehouses")
			.WithTags("Warehouses");

		mapGroup.MapPost("/beers", WarehouseEndpoints.HandleCreateBeer)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("CreateBeer");

		mapGroup.MapPost("/availability", WarehouseEndpoints.HandleCreateAvailability)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("CreateAvailability");

		mapGroup.MapPost("/beers/deposit", WarehouseEndpoints.HandleAddBeerDeposit)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("AddBeerDeposit");
	}
}