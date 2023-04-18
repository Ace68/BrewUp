using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class BeersAvailabilityAsked : DomainEvent
{
	public readonly WarehouseId WarehouseId;

	public readonly IEnumerable<BeerAvailability> Availabilities;


	public BeersAvailabilityAsked(WarehouseId aggregateId, Guid correlationId,
		IEnumerable<BeerAvailability> availabilities) : base(aggregateId, correlationId)
	{
		WarehouseId = aggregateId;
		Availabilities = availabilities;
	}
}