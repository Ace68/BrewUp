using Brewup.Shared.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Brewup.Shared;

public static class SharedHelper
{
	public static IServiceCollection AddAsharedServices(this IServiceCollection services)
	{
		services.AddSingleton<ValidationHandler>();

		return services;
	}
}