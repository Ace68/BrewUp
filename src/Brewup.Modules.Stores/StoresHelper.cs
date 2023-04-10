using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Concretes;
using Brewup.Modules.Stores.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Modules.Stores;

public static class StoresHelper
{
	public static IServiceCollection AddStoresModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SpareAvailabilityValidator>();

		services.AddScoped<ISparesAvailabilityService, SparesAvailabilityService>();
		services.AddScoped<IStoresOrchestrator, StoresOrchestrator>();

		return services;
	}
}