using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class CreateBeer : Command
{
	public readonly BeerId BeerId;
	public readonly BeerName Name;

	public CreateBeer(BeerId aggregateId, BeerName beerName) : base(aggregateId)
	{
		BeerId = aggregateId;
		Name = beerName;
	}
}