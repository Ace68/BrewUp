using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Warehouse.Endpoint;

public static class WarehouseEndpoints
{
	public static async Task<IResult> HandleCreateWarehouse(
		IWarehouseOrchestrator storesOrchestrator,
		IValidator<WarehouseJson> validator,
		ValidationHandler validationHandler,
		WarehouseJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		try
		{
			var warehouseId = await storesOrchestrator.CreateWarehouse(body, cancellationToken);
			return Results.Created($"/api/v1/warehouses/{warehouseId}", warehouseId);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	public static async Task<IResult> HandleAddBeerDeposit(
		IWarehouseOrchestrator storesOrchestrator,
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

	public static async Task<IResult> HandleGetBeers(
		IWarehouseOrchestrator warehouseOrchestrator,
		CancellationToken cancellationToken)
	{
		try
		{
			var beers = await warehouseOrchestrator.GetBeersAsync(cancellationToken);
			return Results.Ok(beers);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}

	public static async Task<IResult> HandleGetWarehouses(
		IWarehouseOrchestrator warehouseOrchestrator,
		CancellationToken cancellationToken)
	{
		try
		{
			var beers = await warehouseOrchestrator.GetBeersAsync(cancellationToken);
			return Results.Ok(beers);
		}
		catch (Exception ex)
		{
			return Results.BadRequest(ex.Message);
		}
	}
}