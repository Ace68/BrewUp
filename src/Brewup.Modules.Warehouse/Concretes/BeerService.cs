using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Concretes;

internal sealed class BeerService : WarehouseBaseService, IBeerService
{
	public BeerService(ILoggerFactory loggerFactory,
		IServiceProvider serviceProvider) : base(loggerFactory, serviceProvider)
	{
	}

	public async Task<string> CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken)
	{
		try
		{
			var beer = await Persister.GetByIdAsync<WarehouseBeer>(beerId.Value, cancellationToken);
			if (!string.IsNullOrWhiteSpace(beer.Id))
				return beer.Id;

			beer = WarehouseBeer.CreateBeer(beerId, beerName);
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
			var beer = await Persister.GetByIdAsync<WarehouseBeer>(beerId.Value, cancellationToken);

			beer.UpdateStoreQuantity(stock, availability);
			var propertiesToUpdate = new Dictionary<string, object>
			{
				{ "Stock", beer.Stock },
				{ "Availability", beer.Availability }
			};
			await Persister.UpdateOneAsync<WarehouseBeer>(beer.Id, propertiesToUpdate, cancellationToken);
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
			var beer = await Persister.GetByIdAsync<WarehouseBeer>(beerId.ToString(), cancellationToken);
			return beer.ToJson();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task<IEnumerable<BeerJson>> GetBeersAsync(CancellationToken cancellationToken)
	{
		try
		{
			var beers = await Persister.FindAsync<WarehouseBeer>();
			var beersArray = beers as WarehouseBeer[] ?? beers.ToArray();

			return beersArray.Any()
				? beersArray.Select(beer => beer.ToJson())
				: Enumerable.Empty<BeerJson>();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}