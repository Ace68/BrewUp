using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Shared.Commands;

public sealed class AddBeerDeposit : CreateStoreMovement
{
	public AddBeerDeposit(BeerId aggregateId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, MovementQuantity movementQuantity) : base(aggregateId,
		storeId, movementId, movementDate, causalId, causalDescription, movementQuantity)
	{
	}
}