using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Stores.Shared.Commands;

public abstract class CreateStoreMovement : Command
{
	public readonly BeerId BeerId;
	public readonly StoreId StoreId;
	public readonly MovementId MovementId;
	public readonly MovementDate MovementDate;
	public readonly CausalId CausalId;
	public readonly CausalDescription CausalDescription;
	public readonly MovementQuantity MovementQuantity;

	protected CreateStoreMovement(BeerId aggregateId, StoreId storeId, MovementId movementId, MovementDate movementDate,
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