using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Shared.DomainEvents;

public sealed class BeerDepositAdded : StoreMovementCreated
{
	public BeerDepositAdded(BeerId aggregateId, StoreId storeId, MovementId movementId, MovementDate movementDate,
		MovementQuantity movementQuantity, CausalId causalId, CausalDescription causalDescription, Stock stock,
		Availability availability, SalesCommitted salesCommitted, ProductionCommitted productionCommitted,
		SupplierOrdered supplierOrdered) : base(aggregateId, storeId, movementId, movementDate, movementQuantity,
		causalId, causalDescription, stock, availability, salesCommitted, productionCommitted, supplierOrdered)
	{
	}
}