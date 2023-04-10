﻿using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Stores.Shared.DomainEvents;

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