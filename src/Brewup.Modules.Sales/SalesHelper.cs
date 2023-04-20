using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Concretes;
using Brewup.Modules.Sales.Sagas;
using Brewup.Modules.Sales.Validators;
using Brewup.Modules.Shared.DomainEvents;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

namespace Brewup.Modules.Sales;

public static class SalesHelper
{
	public static IServiceCollection AddSalesModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SalesOrderValidator>();

		services.AddScoped<ISalesService, SalesService>();
		services.AddScoped<ISalesOrchestrator, SalesOrchestrator>();

		services.AddScoped<IDomainEventHandlerAsync<BeersAvailabilityAsked>, PurchaseOrderSaga>();

		return services;
	}
}