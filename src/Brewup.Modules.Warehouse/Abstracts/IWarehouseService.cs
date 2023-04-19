using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface IWarehouseService
{
	Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName,
		CancellationToken cancellationToken);
}