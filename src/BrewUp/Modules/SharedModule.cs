using Brewup.Shared;

namespace Brewup.Modules;

public class SharedModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddAsharedServices();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// do nothing
	}
}