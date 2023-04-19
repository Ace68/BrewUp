using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class BeerDepositAdded : DomainEvent
{
	public readonly WarehouseId WarehouseId;

	public readonly MovementId MovementId;
	public readonly MovementDate MovementDate;

	public readonly CausalId CausalId;
	public readonly CausalDescription CausalDescription;

	public readonly IEnumerable<BeerAvailabilityUpdated> Rows;

	public BeerDepositAdded(WarehouseId aggregateId, MovementId movementId, MovementDate movementDate,
		CausalId causalId, CausalDescription causalDescription, IEnumerable<BeerAvailabilityUpdated> rows) : base(aggregateId)
	{
		WarehouseId = aggregateId;

		MovementId = movementId;
		MovementDate = movementDate;

		CausalId = causalId;
		CausalDescription = causalDescription;

		Rows = rows;
	}
}