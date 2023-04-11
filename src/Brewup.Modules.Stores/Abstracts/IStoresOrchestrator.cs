using Brewup.Modules.Stores.Shared.Dtos;

namespace Brewup.Modules.Stores.Abstracts;

public interface IStoresOrchestrator
{
	Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken);
	Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken);

	Task CreateAvailabilityAsync(SpareAvailabilityJson body, CancellationToken cancellationToken);
}