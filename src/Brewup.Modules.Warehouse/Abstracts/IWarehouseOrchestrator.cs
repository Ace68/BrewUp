using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IWarehouseOrchestrator
{
	Task<string> CreateWarehouse(WarehouseJson warehouseToCreate, CancellationToken cancellationToken);

	Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken);

	Task<IEnumerable<BeerWithdrawn>> WithdrawalBeerFromWarehouseAsync(WarehouseId warehouseId, IEnumerable<BeerToDrawn> beersToDrawn,
		CancellationToken cancellationToken);
}