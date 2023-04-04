using Brewup.Modules.Purchases.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Purchases.Endpoints;

public static class PurchasesEndpoints
{
	public static async Task<IResult> HandleAddOrder(
		IValidator<Order> validator,
		ValidationHandler validationHandler,
		Order body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		return Results.Ok();
	}
}