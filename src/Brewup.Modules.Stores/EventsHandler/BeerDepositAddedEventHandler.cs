using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.EventsHandler;

public sealed class BeerDepositAddedEventHandler : DomainEventHandlerAsync<BeerDepositAdded>
{
	private readonly IBeerService _beerService;

	public BeerDepositAddedEventHandler(ILoggerFactory loggerFactory,
		IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override async Task HandleAsync(BeerDepositAdded @event, CancellationToken cancellationToken = new())
	{
		try
		{
			await _beerService.UpdateStoreQuantityAsync(@event.BeerId, @event.Stock, @event.Availability,
				cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}