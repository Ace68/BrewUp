using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class BeerAvailabilityChecked : DomainEvent
{
	public readonly WarehouseId WarehouseId;

	public readonly IEnumerable<BeerAvailability> Availabilities;

	public BeerAvailabilityChecked(WarehouseId aggregateId, IEnumerable<BeerAvailability> availabilities) :
		base(aggregateId)
	{
		WarehouseId = aggregateId;
		Availabilities = availabilities;
	}
}