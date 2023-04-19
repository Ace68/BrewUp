namespace Brewup.Modules.Shared.CustomTypes;

public record BeerAvailability(BeerId BeerId,
	Stock Stock,
	Availability Availability,
	ProductionCommitted ProductionCommitted,
	SalesCommitted SalesCommitted,
	SupplierOrdered SupplierOrdered);