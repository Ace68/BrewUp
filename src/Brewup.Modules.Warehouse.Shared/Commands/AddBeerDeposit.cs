using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class AddBeerDeposit : Command
{
	public readonly WarehouseId WarehouseId;

	public readonly MovementId MovementId;
	public readonly MovementDate MovementDate;

	public readonly CausalId CausalId;
	public readonly CausalDescription CausalDescription;

	public readonly IEnumerable<BeerDepositRow> Rows;

	public AddBeerDeposit(WarehouseId aggregateId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, IEnumerable<BeerDepositRow> rows) : base(aggregateId)
	{
		WarehouseId = aggregateId;

		MovementId = movementId;
		MovementDate = movementDate;

		CausalId = causalId;
		CausalDescription = causalDescription;

		Rows = rows;
	}
}