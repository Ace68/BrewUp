using Brewup.Modules.Stores.Shared.Dtos;

namespace Brewup.Modules.Stores.Abstracts;

public interface IBeerService
{
	Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken);
}