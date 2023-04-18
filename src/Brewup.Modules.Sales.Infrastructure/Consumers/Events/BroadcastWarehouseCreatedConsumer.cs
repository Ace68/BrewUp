using Brewup.Modules.Sales.Shared.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Events;

public sealed class BroadcastWarehouseCreatedConsumer : IntegrationEventConsumerBase<BroadcastWarehouseCreated>
{
	protected override IEnumerable<IIntegrationEventHandlerAsync<BroadcastWarehouseCreated>> HandlersAsync { get; }

	public BroadcastWarehouseCreatedConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IIntegrationEventHandlerAsync<BroadcastWarehouseCreated>>();
	}
}