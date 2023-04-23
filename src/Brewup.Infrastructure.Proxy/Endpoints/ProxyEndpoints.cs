using Brewup.Infrastructure.Proxy.Abstracts;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Infrastructure.Proxy.Endpoints;

public static class ProxyEndpoints
{
	public static async Task<IResult> HandleAddOrder(
		IPurchaseOrderSaga purchaseOrderSaga,
		IValidator<SalesOrderJson> validator,
		ValidationHandler validationHandler,
		SalesOrderJson body,
		CancellationToken cancellationToken
	)
	{
		await validationHandler.ValidateAsync(validator, body);
		if (!validationHandler.IsValid)
			return Results.BadRequest(validationHandler.Errors);

		await purchaseOrderSaga.StartAsync(body, cancellationToken);

		return Results.Ok();
	}
}