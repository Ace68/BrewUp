using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Stores.Shared.DomainEvents;

public class SpareAvailabilityCreated : DomainEvent
{
	public readonly SpareId SpareId;
	public readonly Stock Stock;
	public readonly Availability Availability;
	public readonly ProductionCommitted ProductionCommitted;
	public readonly SalesCommitted SalesCommitted;
	public readonly SupplierOrdered SupplierOrdered;

	public SpareAvailabilityCreated(SpareId aggregateId,
		Stock stock,
		Availability availability,
		ProductionCommitted productionCommitted,
		SalesCommitted salesCommitted,
		SupplierOrdered supplierOrdered) : base(aggregateId)
	{
		SpareId = aggregateId;
		Stock = stock;
		Availability = availability;
		ProductionCommitted = productionCommitted;
		SalesCommitted = salesCommitted;
		SupplierOrdered = supplierOrdered;
	}
}