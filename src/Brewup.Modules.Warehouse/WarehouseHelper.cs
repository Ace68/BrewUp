﻿using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Concretes;
using Brewup.Modules.Warehouse.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Modules.Warehouse;

public static class WarehouseHelper
{
	public static IServiceCollection AddWarehouseModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SpareAvailabilityValidator>();

		services.AddScoped<IWarehouseService, WarehouseService>();
		services.AddScoped<ISparesAvailabilityService, SparesAvailabilityService>();
		services.AddScoped<IBeerService, BeerService>();
		services.AddScoped<IWarehouseOrchestrator, WarehouseOrchestrator>();

		return services;
	}
}