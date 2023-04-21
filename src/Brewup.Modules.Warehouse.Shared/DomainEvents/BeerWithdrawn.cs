using Brewup.Modules.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Shared.DomainEvents;

public sealed class BeerWithdrawn : DomainEvent
{
	public readonly WarehouseId WarehouseId;
	public readonly IEnumerable<BeerToDrawn> Beers;

	public BeerWithdrawn(WarehouseId aggregateId, Guid commitId, IEnumerable<BeerToDrawn> beers)
		: base(aggregateId, commitId)
	{
		WarehouseId = aggregateId;
		Beers = beers;
	}
}