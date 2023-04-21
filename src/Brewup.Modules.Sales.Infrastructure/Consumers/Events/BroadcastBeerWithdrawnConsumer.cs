using Brewup.Modules.Shared.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Events;

public sealed class BroadcastBeerWithdrawnConsumer : IntegrationEventConsumerBase<BroadcastBeerWithdrawn>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BroadcastBeerWithdrawn>> HandlersAsync { get; }

	public BroadcastBeerWithdrawnConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IIntegrationEventHandlerAsync<BroadcastBeerWithdrawn>>();
	}
}