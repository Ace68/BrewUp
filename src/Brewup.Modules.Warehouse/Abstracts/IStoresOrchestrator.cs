using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IStoresOrchestrator
{
	Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken);
	Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken);

	Task CreateAvailabilityAsync(SpareAvailabilityJson body, CancellationToken cancellationToken);
}