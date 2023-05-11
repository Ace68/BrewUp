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

		mapGroup.MapPost("", WarehouseEndpoints.HandleCreateWarehouse)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("CreateWarehouse");

		mapGroup.MapGet("", WarehouseEndpoints.HandleGetWarehouses)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("GetWarehouses");

		mapGroup.MapPost("/deposit", WarehouseEndpoints.HandleAddBeerDeposit)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("AddBeerDeposit");

		mapGroup.MapGet("/beers", WarehouseEndpoints.HandleGetBeers)
			.Produces(StatusCodes.Status400BadRequest)
			.Produces(StatusCodes.Status200OK)
			.WithName("GetBeers");
	}
}