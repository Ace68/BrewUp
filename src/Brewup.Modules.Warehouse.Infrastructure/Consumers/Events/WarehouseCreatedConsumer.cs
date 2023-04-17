using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Events;

public sealed class WarehouseCreatedConsumer : DomainEventConsumerBase<WarehouseCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<WarehouseCreated>> HandlersAsync { get; }

	public WarehouseCreatedConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<WarehouseCreated>>();
	}
}