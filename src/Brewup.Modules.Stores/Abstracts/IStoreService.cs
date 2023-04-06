using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Abstracts;

public interface IStoreService
{
	Task CreateAvailabilityAsync(SpareId spareId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered,
		CancellationToken cancellationToken);
}