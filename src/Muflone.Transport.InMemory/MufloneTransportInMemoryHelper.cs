using Microsoft.Extensions.DependencyInjection;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Abstracts;

namespace Muflone.Transport.InMemory;

public static class MufloneTransportInMemoryHelper
{
	public static IServiceCollection AddMufloneTransportInMemory(this IServiceCollection services,
		IEnumerable<IConsumer> messageConsumers)
	{
		services.AddSingleton<IServiceBus, ServiceBus>();
		services.AddSingleton<IEventBus, ServiceBus>();

		foreach (var consumer in messageConsumers)
		{
			consumer.StartAsync(CancellationToken.None);
		}

		return services;
	}
}