using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IWarehouseOrchestrator
{
	Task<string> CreateWarehouse(WarehouseJson warehouseToCreate, CancellationToken cancellationToken);

	Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken);
}