using Brewup.Models;
using FluentValidation;

namespace Brewup.Modules;

public sealed class BrewUpModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		// no services
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		var mapGroup = endpoints.MapGroup("v1/brewup")
			.WithTags("BrewUp");

		mapGroup.MapPost("", SayHelloAsync)
			.Produces(StatusCodes.Status202Accepted)
			.ProducesValidationProblem()
			.WithName("GetHelloParameters");
	}

	private static async Task<IResult> SayHelloAsync(
		HelloRequest helloRequest,
		IValidator<HelloRequest> validator)
	{
		var validationResult = await validator.ValidateAsync(helloRequest);
		if (validationResult.IsValid)
			return Results.Ok($"Hello {helloRequest.Name} from BrewUp");

		var errors = validationResult.Errors.GroupBy(e => e.PropertyName)
			.ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());

		return Results.ValidationProblem(errors);
	}
}