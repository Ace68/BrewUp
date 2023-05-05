using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IWarehouseService
{
	Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName,
		CancellationToken cancellationToken);
	Task<IEnumerable<WarehouseJson>> GetWarehousesAsync(CancellationToken cancellationToken);
}