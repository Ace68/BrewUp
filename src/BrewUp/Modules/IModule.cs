namespace Brewup.Modules;

public interface IModule
{
	bool IsEnabled { get; }
	int Order { get; }

	void RegisterModule(WebApplicationBuilder builder);
	void MapEndpoints(IEndpointRouteBuilder endpoints);
}