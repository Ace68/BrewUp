using Brewup.Modules.Purchases;
using Brewup.Modules.Purchases.Endpoints;

namespace Brewup.Modules;

public class PurchaseModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddPurchaseModule();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("v1/purchases")
			.WithTags("Purchases");

		mapGroup.MapPost("", PurchasesEndpoints.HandleAddOrder)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("AddOrder");
	}
}