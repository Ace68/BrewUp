using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Muflone.Core;

namespace Brewup.Modules.Warehouse.Core.Entities;

public class Warehouse : AggregateRoot
{
	private WarehouseId _warehouseId;
	private WarehouseName _warehouseName;

	private IEnumerable<Beer> _beers;
	private IEnumerable<WarehouseMovement> _movements;

	protected Warehouse()
	{ }

	#region ctor
	internal static Warehouse CreateWarehouse(WarehouseId warehouseId, WarehouseName warehouseName)
	{
		return new Warehouse(warehouseId, warehouseName);
	}

	private Warehouse(WarehouseId warehouseId, WarehouseName warehouseName)
	{
		RaiseEvent(new WarehouseCreated(warehouseId, warehouseName));
	}

	private void Apply(WarehouseCreated @event)
	{
		Id = @event.AggregateId;

		_warehouseId = @event.WarehouseId;
		_warehouseName = @event.WarehouseName;

		_beers = Enumerable.Empty<Beer>();
		_movements = Enumerable.Empty<WarehouseMovement>();
	}
	#endregion

	#region AddBeerDeposit
	internal void AddBeerDeposit(MovementId movementId, MovementDate movementDate, CausalId causalId,
		CausalDescription causalDescription, IEnumerable<BeerDepositRow> rows)
	{
		var beersAvailability = Enumerable.Empty<BeerAvailabilityUpdated>();
		foreach (var row in rows)
		{
			var beer = _beers.FirstOrDefault(x => x.BeerId == row.BeerId);
			var stock = new Stock(row.MovementQuantity.Value);
			var availability = new Availability(row.MovementQuantity.Value);
			var productionCommitted = new ProductionCommitted(0);
			var salesCommitted = new SalesCommitted(0);
			var supplierOrdered = new SupplierOrdered(0);
			if (beer != null)
			{
				stock = new Stock(Value: beer.GetStock().Value + row.MovementQuantity.Value);
				availability = new Availability(Value: stock.Value - beer.GetProductionCommitted().Value - beer.GetSalesCommitted().Value + beer.GetSupplierOrdered().Value);
				productionCommitted = beer.GetProductionCommitted();
				salesCommitted = beer.GetSalesCommitted();
				supplierOrdered = beer.GetSupplierOrdered();
			}

			beersAvailability = beersAvailability.Concat(new List<BeerAvailabilityUpdated>
			{
				new (row.BeerId,
					row.BeerName,
					row.MovementQuantity,
					stock,
					availability,
					productionCommitted,
					salesCommitted,
					supplierOrdered)
			});
		}

		RaiseEvent(new BeerDepositAdded(_warehouseId, movementId, movementDate, causalId, causalDescription, beersAvailability));
	}

	private void Apply(BeerDepositAdded @event)
	{
		_movements = _movements.Append(WarehouseMovement.CreateWarehouseMovement(_warehouseId,
			@event.MovementId,
			@event.MovementDate,
			@event.CausalId,
			@event.CausalDescription,
			@event.Rows.Select(r => new BeerDepositRow(r.BeerId, r.BeerName, r.MovementQuantity))));

		foreach (var row in @event.Rows)
		{
			var beer = _beers.FirstOrDefault(x => x.BeerId == row.BeerId);
			if (beer == null)
			{
				_beers = _beers.Append(Beer.CreateBeer(row.BeerId, row.BeerName));
				beer = _beers.FirstOrDefault(x => x.BeerId == row.BeerId);
			}

			beer.UpdateAvailabilities(row.Stock);
		}
	}
	#endregion

	#region AskForBeerAvailability
	internal void AskForBeersAvailability(IEnumerable<BeerId> beers, Guid correlationId)
	{
		var availabilities = Enumerable.Empty<BeerAvailability>();
		foreach (var beerId in beers)
		{
			var beer = _beers.FirstOrDefault(x => x.BeerId == beerId);
			if (beer != null)
			{
				availabilities = availabilities.Concat(new List<BeerAvailability>
				{
					new (beerId, beer.GetStock(), beer.GetAvailability(),
						beer.GetProductionCommitted(), beer.GetSalesCommitted(), beer.GetSupplierOrdered())
				});
			}
			else
			{
				availabilities = availabilities.Concat(new List<BeerAvailability>
				{
					new (beerId, new Stock(0), new Availability(0),
						new ProductionCommitted(0), new SalesCommitted(0), new SupplierOrdered(0))
				});
			}
		}

		RaiseEvent(new BeersAvailabilityAsked(_warehouseId, correlationId, availabilities));
	}

	private void Apply(BeersAvailabilityAsked @event)
	{
		// do nothing
	}
	#endregion

	#region WithdrawnFromWarehouse
	internal void WithdrawnFromWarehouse(IEnumerable<BeerToDrawn> beers, Guid commitId)
	{
		var beersDrawn = Enumerable.Empty<BeerToDrawn>();

		foreach (var beerToDrawn in beers)
		{
			var beer = _beers.FirstOrDefault(x => x.BeerId == beerToDrawn.BeerId);

			if (beer != null)
			{
				Stock stock;
				Availability availability;
				if (beer.GetAvailability().Value >= beerToDrawn.Quantity.Value)
				{
					stock = new Stock(beer.GetStock().Value - beerToDrawn.Quantity.Value);
					availability = new Availability(stock.Value - beer.GetProductionCommitted().Value - beer.GetSalesCommitted().Value + beer.GetSupplierOrdered().Value);
					beersDrawn = beersDrawn.Concat(new List<BeerToDrawn>
					{
						beerToDrawn with { Stock = stock, Availability = availability }
					});
				}

				if (beer.GetAvailability().Value < beerToDrawn.Quantity.Value)
				{
					stock = new Stock(beer.GetStock().Value - beer.GetAvailability().Value);
					availability = new Availability(stock.Value - beer.GetProductionCommitted().Value - beer.GetSalesCommitted().Value + beer.GetSupplierOrdered().Value);
					beersDrawn = beersDrawn.Concat(new List<BeerToDrawn>
					{
						beerToDrawn with
						{
							Quantity = new Quantity(beerToDrawn.Quantity.Value - beer.GetAvailability().Value),
							Stock = stock,
							Availability = availability
						}
					});
				}

				if (beer.GetAvailability().Value == 0)
				{
					stock = beer.GetStock();
					availability = beer.GetAvailability();
					beersDrawn = beersDrawn.Concat(new List<BeerToDrawn>
					{
						beerToDrawn with
						{
							Quantity = new Quantity(0),
							Stock = stock,
							Availability = availability
						}
					});
				}
			}
			else
			{
				beersDrawn = beersDrawn.Concat(new List<BeerToDrawn>
				{
					beerToDrawn with
					{
						Quantity = new Quantity(0),
						Stock = new Stock(0),
						Availability = new Availability(0)
					}
				});
			}

			RaiseEvent(new BeerWithdrawn(_warehouseId, commitId, beersDrawn));
		}
	}

	private void Apply(BeerWithdrawn @event)
	{
		foreach (var beerToDrawn in @event.Beers)
		{
			var beer = _beers.FirstOrDefault(x => x.BeerId == beerToDrawn.BeerId);

			beer?.UpdateAvailabilities(beerToDrawn.Stock);
		}
	}
	#endregion
}