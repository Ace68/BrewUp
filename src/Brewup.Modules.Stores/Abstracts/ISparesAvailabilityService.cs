using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Modules.Stores.Abstracts;

public interface ISparesAvailabilityService
{
	Task CreateAvailabilityAsync(SpareId spareId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered,
		CancellationToken cancellationToken);

	Task<SpareAvailabilityJson> GetAvailabilityAsync(SpareId spareId, CancellationToken cancellationToken);
}