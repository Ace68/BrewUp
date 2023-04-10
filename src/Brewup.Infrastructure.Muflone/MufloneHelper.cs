using Brewup.Infrastructure.Muflone.Consumers;
using Brewup.Modules.Stores.EventsHandler;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory;
using Muflone.Transport.InMemory.Abstracts;

namespace Brewup.Infrastructure.Muflone;

public static class MufloneHelper
{
	public static IServiceCollection AddMuflone(this IServiceCollection services)
	{
		services.AddScoped<IDomainEventHandlerAsync<SparesAvailabilityCreated>, SparesAvailabilityCreatedEventHandler>();

		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		var repository = serviceProvider.GetService<IRepository>();

		var consumers = new List<IConsumer>
		{
			new CreateSpareAvailabilityConsumer(repository!, loggerFactory!),
			new SparesAvailabilityCreatedConsumer(serviceProvider, loggerFactory!),

			new AskForAvailabilityConsumer(repository!, loggerFactory!)
		};

		services.AddMufloneTransportInMemory(consumers);

		return services;
	}
}