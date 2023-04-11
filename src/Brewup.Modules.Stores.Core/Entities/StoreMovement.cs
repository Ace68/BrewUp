using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Core;

namespace Brewup.Modules.Stores.Core.Entities;

public class StoreMovement : Entity
{
	private BeerId _beerId;
	private StoreId _storeId;

	private MovementId _movementId;
	private MovementDate _movementDate;

	private CausalId _causalId;
	private CausalDescription _causalDescription;

	private MovementQuantity _movementQuantity;

	protected StoreMovement()
	{ }

	internal static StoreMovement CreateStoreMovement(BeerId beerId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, MovementQuantity movementQuantity)
	{
		return new StoreMovement(
			beerId,
			storeId,
			movementId,
			movementDate,
			causalId,
			causalDescription,
			movementQuantity
		);
	}

	private StoreMovement(BeerId beerId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, MovementQuantity movementQuantity)
	{
		_beerId = beerId;
		_storeId = storeId;

		_movementId = movementId;
		_movementDate = movementDate;

		_causalId = causalId;
		_causalDescription = causalDescription;

		_movementQuantity = movementQuantity;
	}
}