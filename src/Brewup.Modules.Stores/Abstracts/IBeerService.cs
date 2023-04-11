using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Abstracts;

public interface IBeerService
{
	Task<string> CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken);
	Task UpdateStoreQuantityAsync(BeerId beerId, Stock stock, Availability availability,
		CancellationToken cancellationToken);

	Task<BeerJson> GetBeerAsync(BeerId beerId, CancellationToken cancellationToken);
}