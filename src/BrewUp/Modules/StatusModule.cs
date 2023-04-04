namespace Brewup.Modules;

public sealed class StatusModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		// no services
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/-/healthz", HandleStatus)
			.Produces(StatusCodes.Status204NoContent)
			.WithTags("Status");

		endpoints.MapGet("/-/ready", HandleStatus)
			.Produces(StatusCodes.Status204NoContent)
			.WithTags("Status");

		endpoints.MapGet("/-/check-up", HandleStatus)
			.Produces(StatusCodes.Status204NoContent)
			.WithTags("Status");
	}

	private static IResult HandleStatus()
	{
		return Results.NoContent();
	}
}