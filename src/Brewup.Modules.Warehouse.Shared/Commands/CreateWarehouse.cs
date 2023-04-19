using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class CreateWarehouse : Command
{
	public readonly WarehouseId WarehouseId;
	public readonly WarehouseName WarehouseName;

	public CreateWarehouse(WarehouseId aggregateId, WarehouseName warehouseName) : base(aggregateId)
	{
		WarehouseId = aggregateId;
		WarehouseName = warehouseName;
	}
}