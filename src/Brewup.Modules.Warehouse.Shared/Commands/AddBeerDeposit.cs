using Brewup.Modules.Warehouse.Shared.ValueObjects;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class AddBeerDeposit : CreateWarehouseMovement
{
	public AddBeerDeposit(BeerId aggregateId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, MovementQuantity movementQuantity) : base(aggregateId,
		storeId, movementId, movementDate, causalId, causalDescription, movementQuantity)
	{
	}
}