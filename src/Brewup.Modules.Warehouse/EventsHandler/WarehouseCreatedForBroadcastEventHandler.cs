using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Shared.IntegrationEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone;

namespace Brewup.Modules.Warehouse.EventsHandler;

public class WarehouseCreatedForBroadcastEventHandler : DomainEventHandlerAsync<WarehouseCreated>
{
	private readonly IEventBus _eventBus;

	public WarehouseCreatedForBroadcastEventHandler(ILoggerFactory loggerFactory,
		IEventBus eventBus) : base(loggerFactory)
	{
		_eventBus = eventBus;
	}

	public override async Task HandleAsync(WarehouseCreated @event, CancellationToken cancellationToken = new())
	{
		try
		{
			var broadcastWarehouseCreated = new BroadcastWarehouseCreated(@event.WarehouseId, @event.WarehouseName);
			await _eventBus.PublishAsync(broadcastWarehouseCreated, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}