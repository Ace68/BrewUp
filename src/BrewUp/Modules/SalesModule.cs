using Brewup.Infrastructure.Proxy.Endpoints;
using Brewup.Modules.Sales;

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

		mapGroup.MapPost("", ProxyEndpoints.HandleAddOrder)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("AddOrder");
	}
}