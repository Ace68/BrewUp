using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Concretes;
using Brewup.Modules.Sales.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Modules.Sales;

public static class SalesHelper
{
	public static IServiceCollection AddSalesModule(this IServiceCollection services)
	{
		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<SalesOrderValidator>();

		services.AddScoped<ISalesOrderService, SalesOrderService>();
		services.AddScoped<ISalesOrchestrator, SalesOrchestrator>();

		return services;
	}
}