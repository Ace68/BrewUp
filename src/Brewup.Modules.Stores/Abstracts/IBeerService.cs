using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Abstracts;

public interface IBeerService
{
	Task<string> CreateBeerAsync(BeerId beerId, BeerName beerName, CancellationToken cancellationToken);
}