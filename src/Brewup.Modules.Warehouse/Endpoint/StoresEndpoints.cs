using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Warehouse.Endpoint;

public static class WarehouseEndpoints
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
			return Results.BadRequest(ex.Message);
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
			return Results.BadRequest(ex.Message);
			throw;
		}

		return Results.Accepted();
	}

	public static async Task<IResult> HandleAddBeerDeposit(
		IStoresOrchestrator storesOrchestrator,
		IValidator<BeerDepositJson> validator,
		ValidationHandler validationHandler,
		BeerDepositJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		try
		{
			await storesOrchestrator.AddBeerDepositAsync(body, cancellationToken);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}

		return Results.Accepted();
	}

}