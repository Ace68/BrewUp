using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.EventsHandler;

public class BeerCreatedEventHandler : DomainEventHandlerAsync<BeerCreated>
{
	private readonly IBeerService _beerService;

	public BeerCreatedEventHandler(ILoggerFactory loggerFactory,
		IBeerService beerService) : base(loggerFactory)
	{
		_beerService = beerService;
	}

	public override async Task HandleAsync(BeerCreated @event, CancellationToken cancellationToken = new())
	{
		try
		{
			await _beerService.CreateBeerAsync(@event.BeerId, @event.BeerName, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}