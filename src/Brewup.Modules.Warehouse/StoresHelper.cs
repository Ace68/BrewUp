using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Concretes;
using Brewup.Modules.Warehouse.EventsHandler;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse;

public static class WarehouseHelper
{
	public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SpareAvailabilityValidator>();

		services.AddScoped<ISparesAvailabilityService, SparesAvailabilityService>();
		services.AddScoped<IBeerService, BeerService>();
		services.AddScoped<IStoresOrchestrator, StoresOrchestrator>();

		services.AddScoped<IDomainEventHandlerAsync<SparesAvailabilityCreated>, SparesAvailabilityCreatedEventHandler>();
		services.AddScoped<IDomainEventHandlerAsync<BeerCreated>, BeerCreatedEventHandler>();
		services.AddScoped<IDomainEventHandlerAsync<BeerDepositAdded>, BeerDepositAddedEventHandler>();

		return services;
	}
}