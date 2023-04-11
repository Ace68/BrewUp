using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Core;

namespace Brewup.Modules.Stores.Core.Entities;

public class Beer : AggregateRoot
{
	private BeerId _beerId;
	private BeerName _beerName;

	protected Beer() { }

	#region constructor
	internal static Beer CreateBeer(BeerId beerId, BeerName beerName)
	{
		return new Beer(beerId, beerName);
	}

	private Beer(BeerId beerId, BeerName beerName)
	{
		RaiseEvent(new BeerCreated(beerId, beerName));
	}

	private void Apply(BeerCreated @event)
	{
		Id = @event.AggregateId;

		_beerId = @event.BeerId;
		_beerName = @event.BeerName;
	}
	#endregion
}