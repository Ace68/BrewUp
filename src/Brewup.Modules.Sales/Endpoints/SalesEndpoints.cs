using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Sales.Endpoints;

public static class SalesEndpoints
{
	public static async Task<IResult> HandleAddOrder(
		ISalesOrchestrator purchaseOrchestrator,
		IValidator<SalesOrderJson> validator,
		ValidationHandler validationHandler,
		SalesOrderJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		await purchaseOrchestrator.AddOrderAsync(body, cancellationToken);

		return Results.Ok();
	}

	public static async Task<IResult> HandleGetOrders(
		ISalesOrchestrator salesOrchestrator,
		CancellationToken cancellationToken
	)
	{
		var salesOrders = await salesOrchestrator.GetSalesOrdersAsync(cancellationToken);

		return Results.Ok(salesOrders);
	}
}