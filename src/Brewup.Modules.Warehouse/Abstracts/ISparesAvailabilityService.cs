using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Abstracts;

public interface ISparesAvailabilityService
{
	Task CreateAvailabilityAsync(SpareId spareId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered,
		CancellationToken cancellationToken);

	Task<SpareAvailabilityJson> GetAvailabilityAsync(SpareId spareId, CancellationToken cancellationToken);
}