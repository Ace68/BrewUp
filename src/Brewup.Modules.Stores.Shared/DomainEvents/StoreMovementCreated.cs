using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Stores.Shared.DomainEvents;

public abstract class StoreMovementCreated : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly StoreId StoreId;

	public readonly MovementId MovementId;
	public readonly MovementDate MovementDate;
	public readonly MovementQuantity MovementQuantity;

	public readonly CausalId CausalId;
	public readonly CausalDescription CausalDescription;

	public readonly Stock Stock;
	public readonly Availability Availability;
	public readonly SalesCommitted SalesCommitted;
	public readonly ProductionCommitted ProductionCommitted;
	public readonly SupplierOrdered SupplierOrdered;

	protected StoreMovementCreated(BeerId aggregateId, StoreId storeId, MovementId movementId,
		MovementDate movementDate, MovementQuantity movementQuantity, CausalId causalId,
		CausalDescription causalDescription, Stock stock, Availability availability, SalesCommitted salesCommitted,
		ProductionCommitted productionCommitted, SupplierOrdered supplierOrdered) : base(aggregateId)
	{
		BeerId = aggregateId;
		StoreId = storeId;

		MovementId = movementId;
		MovementDate = movementDate;
		MovementQuantity = movementQuantity;

		CausalId = causalId;
		CausalDescription = causalDescription;

		Stock = stock;
		Availability = availability;
		SalesCommitted = salesCommitted;
		ProductionCommitted = productionCommitted;
		SupplierOrdered = supplierOrdered;
	}
}