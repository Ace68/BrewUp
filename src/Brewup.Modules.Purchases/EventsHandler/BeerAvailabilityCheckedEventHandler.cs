using Brewup.Modules.Purchases.Shared.DomainEvents;
using Muflone.Messages.Events;

namespace Brewup.Modules.Purchases.EventsHandler;

public class BeerAvailabilityCheckedEventHandler : DomainEventHandlerAsync<BeerAvailabilityChecked>
{
	public override Task HandleAsync(BeerAvailabilityChecked @event, CancellationToken cancellationToken = new())
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