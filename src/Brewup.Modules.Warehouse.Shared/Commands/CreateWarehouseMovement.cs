using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public abstract class CreateWarehouseMovement : Command
{
	public readonly BeerId BeerId;
	public readonly StoreId StoreId;
	public readonly MovementId MovementId;
	public readonly MovementDate MovementDate;
	public readonly CausalId CausalId;
	public readonly CausalDescription CausalDescription;
	public readonly MovementQuantity MovementQuantity;

	protected CreateWarehouseMovement(BeerId aggregateId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, MovementQuantity movementQuantity) : base(aggregateId)
	{
		BeerId = aggregateId;
		StoreId = storeId;
		MovementId = movementId;
		MovementDate = movementDate;
		CausalId = causalId;
		CausalDescription = causalDescription;
		MovementQuantity = movementQuantity;
	}
}