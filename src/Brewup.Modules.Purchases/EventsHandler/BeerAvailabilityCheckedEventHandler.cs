using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Shared.DomainEvents;
using Muflone.Messages.Events;

namespace Brewup.Modules.Purchases.EventsHandler;

public class BeerAvailabilityCheckedEventHandler : DomainEventHandlerAsync<BeerAvailabilityChecked>
{
	private readonly IPurchaseOrchestrator _purchaseOrchestrator;

	public BeerAvailabilityCheckedEventHandler(IPurchaseOrchestrator purchaseOrchestrator)
	{
		_purchaseOrchestrator = purchaseOrchestrator;
	}

	public override async Task HandleAsync(BeerAvailabilityChecked @event, CancellationToken cancellationToken = new())
	{
		try
		{

		}
		catch (Exception ex)
		{
			throw;
		}
	}
}