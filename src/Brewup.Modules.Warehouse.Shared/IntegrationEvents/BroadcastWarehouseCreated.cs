using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.IntegrationEvents;

public sealed class BroadcastWarehouseCreated : IntegrationEvent
{
	public readonly WarehouseId WarehouseId;
	public readonly WarehouseName WarehouseName;

	public BroadcastWarehouseCreated(WarehouseId aggregateId, WarehouseName warehouseName) : base(aggregateId)
	{
		WarehouseId = aggregateId;
		WarehouseName = warehouseName;
	}
}