using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Core;

namespace Brewup.Modules.Stores.Core.Entities;

public class Beer : AggregateRoot
{
	private BeerId _beerId;
	private BeerName _beerName;

	private Stock _stock;
	private Availability _availability;
	private SalesCommitted _salesCommitted;

	private IEnumerable<StoreMovement> _movements;

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

		_stock = new Stock(0);
		_availability = new Availability(0);
		_salesCommitted = new SalesCommitted(0);

		_movements = Enumerable.Empty<StoreMovement>();
	}
	#endregion

	#region storeDeposit
	internal void AddStoreDeposit(StoreId storeId, MovementId movementId, MovementDate movementDate, CausalId causalId,
		CausalDescription causalDescription, MovementQuantity movementQuantity)
	{
		var stock = new Stock(_stock.Value + movementQuantity.Value);
		var availability = new Availability(stock.Value - _salesCommitted.Value);
		RaiseEvent(new BeerDepositAdded(_beerId, storeId, movementId, movementDate, movementQuantity, causalId,
			causalDescription, stock, availability, _salesCommitted, new ProductionCommitted(0),
			new SupplierOrdered(0)));
	}

	private void Apply(BeerDepositAdded @event)
	{
		var storeMovement = StoreMovement.CreateStoreMovement(_beerId, @event.StoreId, @event.MovementId,
			@event.MovementDate, @event.CausalId, @event.CausalDescription, @event.MovementQuantity);
		_movements = _movements.Append(storeMovement);

		_stock = new Stock(_stock.Value + @event.MovementQuantity.Value);
		_availability = new Availability(_stock.Value - _salesCommitted.Value);
	}
	#endregion

	#region Availability
	internal void CheckAvailability()
	{
		RaiseEvent(new BeerAvailabilityChecked(_beerId, _stock, _availability, new ProductionCommitted(0),
						_salesCommitted, new SupplierOrdered(0)));
	}

	private void Apply(BeerAvailabilityChecked @event)
	{
		// do nothing
	}
	#endregion
}