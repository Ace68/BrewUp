using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Concretes;
using Brewup.Modules.Purchases.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Modules.Purchases;

public static class PurchaseHelper
{
	public static IServiceCollection AddPurchaseModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<OrderValidator>();

		services.AddSingleton<IPurchaseService, PurchaseService>();
		services.AddSingleton<IPurchaseOrchestrator, PurchaseOrchestrator>();

		return services;
	}
}