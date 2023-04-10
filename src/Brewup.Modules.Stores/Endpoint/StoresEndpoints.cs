using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Stores.Endpoint;

public static class StoresEndpoints
{
	public static async Task<IResult> HandleCreateBeer(
		IStoresOrchestrator storesOrchestrator,
		IValidator<BeerJson> validator,
		ValidationHandler validationHandler,
		BeerJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		try
		{
			var beerId = await storesOrchestrator.CreateBeerAsync(body, cancellationToken);
			return Results.Created($"/api/v1/stores/beers/{beerId}", beerId);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}

	public static async Task<IResult> HandleCreateAvailability(
		IStoresOrchestrator storesOrchestrator,
		IValidator<SpareAvailabilityJson> validator,
		ValidationHandler validationHandler,
		SpareAvailabilityJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		try
		{
			await storesOrchestrator.CreateAvailabilityAsync(body, cancellationToken);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}

		return Results.Accepted();
	}
}