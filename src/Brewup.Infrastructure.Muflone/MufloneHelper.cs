using Brewup.Modules.Stores.Core.Consumers;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Shared.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Eventstore;
using Muflone.Transport.InMemory;
using Muflone.Transport.InMemory.Abstracts;

namespace Brewup.Infrastructure.Muflone;

public static class MufloneHelper
{
	public static IServiceCollection AddMuflone(this IServiceCollection services,
		EventStoreSettings eventStoreSettings)
	{
		services.AddMufloneEventStore(eventStoreSettings.ConnectionString);

		services.AddSingleton<ICommandConsumer<CreateSpareAvailability>, CreateSpareAvailabilityConsumer>();

		//var serviceProvider = services.BuildServiceProvider();
		//var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
		//var repository = serviceProvider.GetRequiredService<IRepository>();

		//var consumers = new List<IConsumer>
		//{
		//	new CreateSpareAvailabilityConsumer(loggerFactory)
		//};
		services.AddMufloneTransportInMemory(Enumerable.Empty<IConsumer>());

		return services;
	}
}