using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.EventsHandler;

public sealed class BeerDepositAddedForBeerCreatedEventHandler : DomainEventHandlerAsync<BeerDepositAdded>
{
	private readonly IBeerService _beerService;

	public BeerDepositAddedForBeerCreatedEventHandler(ILoggerFactory loggerFactory,
		IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override async Task HandleAsync(BeerDepositAdded @event, CancellationToken cancellationToken = new())
	{
		try
		{
			foreach (var row in @event.Rows)
			{
				await _beerService.CreateBeerAsync(row.BeerId, row.BeerName, cancellationToken);
				await _beerService.UpdateStoreQuantityAsync(row.BeerId, row.Stock, row.Availability, cancellationToken);
			}
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}