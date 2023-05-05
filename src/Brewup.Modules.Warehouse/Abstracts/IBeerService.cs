using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IBeerService
{
	Task<string> CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken);
	Task UpdateStoreQuantityAsync(BeerId beerId, Stock stock, Availability availability,
		CancellationToken cancellationToken);

	Task<BeerJson> GetBeerAsync(BeerId beerId, CancellationToken cancellationToken);
	Task<IEnumerable<BeerJson>> GetBeersAsync(CancellationToken cancellationToken);
}