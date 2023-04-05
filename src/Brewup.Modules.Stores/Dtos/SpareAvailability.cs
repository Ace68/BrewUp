namespace Brewup.Modules.Stores.Dtos;

public record SpareAvailability(string SpareId, double Stock, double Availability, double ProductionCommitted, double SalesCommitted, double SupplierOrdered);