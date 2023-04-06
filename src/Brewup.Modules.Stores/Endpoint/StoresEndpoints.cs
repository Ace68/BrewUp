using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Shared.Concretes;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Brewup.Modules.Stores.Endpoint;

public static class StoresEndpoints
{
	public static async Task<IResult> HandleCreateAvailability(
		IStoresOrchestrator storesOrchestrator,
		IValidator<SpareAvailability> validator,
		ValidationHandler validationHandler,
		SpareAvailability body,
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