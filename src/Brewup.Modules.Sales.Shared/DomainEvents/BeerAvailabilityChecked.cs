using Brewup.Modules.Sales.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Sales.Shared.DomainEvents;

public sealed class BeerAvailabilityChecked : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly Stock Stock;
	public readonly Availability Availability;
	public readonly ProductionCommitted ProductionCommitted;
	public readonly SalesCommitted SalesCommitted;
	public readonly SupplierOrdered SupplierOrdered;

	public BeerAvailabilityChecked(BeerId aggregateId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered) :
		base(aggregateId)
	{
		BeerId = aggregateId;
		Stock = stock;
		Availability = availability;
		ProductionCommitted = productionCommitted;
		SalesCommitted = salesCommitted;
		SupplierOrdered = supplierOrdered;
	}
}