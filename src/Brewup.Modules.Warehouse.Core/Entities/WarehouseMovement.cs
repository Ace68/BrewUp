using Brewup.Modules.Shared.CustomTypes;
using Muflone.Core;

namespace Brewup.Modules.Warehouse.Core.Entities;

public class WarehouseMovement : Entity
{
	private WarehouseId _warehouseId;

	private MovementId _movementId;
	private MovementDate _movementDate;

	private CausalId _causalId;
	private CausalDescription _causalDescription;

	private readonly IEnumerable<BeerDepositRow> _rows;

	protected WarehouseMovement()
	{ }

	internal static WarehouseMovement CreateWarehouseMovement(WarehouseId warehouseId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, IEnumerable<BeerDepositRow> rows)
	{
		return new WarehouseMovement(
			warehouseId,
			movementId,
			movementDate,
			causalId,
			causalDescription,
			rows
		);
	}

	private WarehouseMovement(WarehouseId warehouseId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, IEnumerable<BeerDepositRow> rows)
	{
		_warehouseId = warehouseId;

		_movementId = movementId;
		_movementDate = movementDate;

		_causalId = causalId;
		_causalDescription = causalDescription;

		_rows = rows;
	}
}