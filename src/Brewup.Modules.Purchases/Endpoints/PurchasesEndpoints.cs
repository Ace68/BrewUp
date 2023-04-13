using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Purchases.Endpoints;

public static class PurchasesEndpoints
{
	public static async Task<IResult> HandleAddOrder(
		IPurchaseOrchestrator purchaseOrchestrator,
		IValidator<PurchaseOrderJson> validator,
		ValidationHandler validationHandler,
		PurchaseOrderJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		await purchaseOrchestrator.AddOrderAsync(body, cancellationToken);

		return Results.Ok();
	}
}