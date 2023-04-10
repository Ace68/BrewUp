using Brewup.Modules.Stores.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Transport.InMemory;
using Muflone.Transport.InMemory.Abstracts;

namespace Brewup.Infrastructure.Muflone;

public static class MufloneHelper
{
	public static IServiceCollection AddMuflone(this IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		var repository = serviceProvider.GetService<IRepository>();

		var consumers = new List<IConsumer>
		{
			new CreateSpareAvailabilityConsumer(repository!, loggerFactory!),
			new AskForAvailabilityConsumer(repository!, loggerFactory!)
		};

		services.AddMufloneTransportInMemory(consumers);

		return services;
	}
}