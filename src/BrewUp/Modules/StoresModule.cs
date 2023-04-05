using Brewup.Modules.Stores;
using Brewup.Modules.Stores.Endpoint;

namespace Brewup.Modules;

public class StoresModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddStoresModule();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("v1/stores")
			.WithTags("Stores");

		mapGroup.MapPost("/availability", StoresEndpoints.HandleCreateAvailability)
			.Produces(StatusCodes.Status400BadRequest)
			.WithName("CreateAvailability");
	}
}