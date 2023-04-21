using Brewup.Modules.Shared.IntegrationEvents;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone;

namespace Brewup.Modules.Warehouse.EventsHandler;

public sealed class BeerWithdrawnEventHandler : DomainEventHandlerAsync<BeerWithdrawn>
{
	private readonly IEventBus _eventBus;

	public BeerWithdrawnEventHandler(ILoggerFactory loggerFactory,
		IEventBus eventBus) : base(loggerFactory)
	{
		_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
	}

	public override async Task HandleAsync(BeerWithdrawn @event, CancellationToken cancellationToken = new())
	{
		try
		{
			var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
			var broadcastBeerWithdrawn = new BroadcastBeerWithdrawn(@event.WarehouseId, correlationId, @event.Beers);
			await _eventBus.PublishAsync(broadcastBeerWithdrawn, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}