namespace Brewup.Modules.Warehouse.Shared.ValueObjects;

public record BeerAvailability(BeerId BeerId,
	Stock Stock,
	Availability Availability,
	ProductionCommitted ProductionCommitted,
	SalesCommitted SalesCommitted,
	SupplierOrdered SupplierOrdered);