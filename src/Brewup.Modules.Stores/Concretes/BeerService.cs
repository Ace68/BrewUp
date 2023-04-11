using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.Concretes;

public sealed class BeerService : StoreBaseService, IBeerService
{
	public BeerService(ILoggerFactory loggerFactory,
		IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task<string> CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken)
	{
		try
		{
			var beer = Beer.CreateBeer(beerId, beerName);
			await Persister.InsertAsync(beer, cancellationToken);

			return beer.Id;
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task UpdateStoreQuantityAsync(BeerId beerId, Stock stock, Availability availability,
		CancellationToken cancellationToken)
	{
		try
		{
			var beer = await Persister.GetByIdAsync<Beer>(beerId.ToString(), cancellationToken);

			beer.UpdateStoreQuantity(stock, availability);
			var propertiesToUpdate = new Dictionary<string, object>
			{
				{ "Stock", beer.Stock },
				{ "Availability", beer.Availability }
			};
			await Persister.UpdateOneAsync<Beer>(beer.Id, propertiesToUpdate, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task<BeerJson> GetBeerAsync(BeerId beerId, CancellationToken cancellationToken)
	{
		try
		{
			var beer = await Persister.GetByIdAsync<Beer>(beerId.ToString(), cancellationToken);
			return beer.ToJson();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}