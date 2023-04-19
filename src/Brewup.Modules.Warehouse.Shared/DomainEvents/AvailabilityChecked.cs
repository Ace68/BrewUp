using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class AvailabilityChecked : DomainEvent
{
	public readonly SpareId SpareId;
	public readonly Availability Availability;

	public AvailabilityChecked(SpareId aggregateId, Availability availability) : base(aggregateId)
	{
		SpareId = aggregateId;
		Availability = availability;
	}
}