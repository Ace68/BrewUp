using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class BeerCreated : DomainEvent
{
	public readonly BeerId BeerId;
	public readonly BeerName BeerName;

	public BeerCreated(BeerId aggregateId, BeerName beerName) : base(aggregateId)
	{
		BeerId = aggregateId;
		BeerName = beerName;
	}
}