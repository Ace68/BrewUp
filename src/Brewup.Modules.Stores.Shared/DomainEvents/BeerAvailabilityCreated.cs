using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Stores.Shared.DomainEvents;

public sealed class BeerAvailabilityCreated : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly Stock Stock;
	public readonly Availability Availability;

	public BeerAvailabilityCreated(BeerId aggregateId, Stock stock, Availability availability) : base(aggregateId)
	{
		BeerId = aggregateId;
		Stock = stock;
		Availability = availability;
	}
}