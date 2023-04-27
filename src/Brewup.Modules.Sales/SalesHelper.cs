using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Concretes;
using Brewup.Modules.Sales.EventsHandler;
using Brewup.Modules.Sales.Sagas;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Modules.Sales.Validators;
using Brewup.Modules.Shared.DomainEvents;
using Brewup.Modules.Shared.IntegrationEvents;
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

		services.AddScoped<ISalesOrderService, SalesOrderService>();
		services.AddScoped<ISalesOrchestrator, SalesOrchestrator>();

		services.AddScoped<IDomainEventHandlerAsync<BeersAvailabilityAsked>, SalesOrderSaga>();
		services.AddScoped<IIntegrationEventHandlerAsync<BroadcastBeerWithdrawn>, SalesOrderSaga>();
		services.AddScoped<IDomainEventHandlerAsync<SalesOrderCreated>, SalesOrderSaga>();
		services.AddScoped<IDomainEventHandlerAsync<SalesOrderCreated>, SalesOrderCreatedEventHandler>();

		return services;
	}
}