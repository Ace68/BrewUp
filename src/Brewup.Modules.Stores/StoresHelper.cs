using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Concretes;
using Brewup.Modules.Stores.EventsHandler;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Messages.Events;

namespace Brewup.Modules.Stores;

public static class StoresHelper
{
	public static IServiceCollection AddStoresModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SpareAvailabilityValidator>();

		services.AddScoped<ISparesAvailabilityService, SparesAvailabilityService>();
		services.AddScoped<IBeerService, BeerService>();
		services.AddScoped<IStoresOrchestrator, StoresOrchestrator>();

		services.AddScoped<IDomainEventHandlerAsync<SparesAvailabilityCreated>, SparesAvailabilityCreatedEventHandler>();
		services.AddScoped<IDomainEventHandlerAsync<BeerCreated>, BeerCreatedEventHandler>();

		return services;
	}
}