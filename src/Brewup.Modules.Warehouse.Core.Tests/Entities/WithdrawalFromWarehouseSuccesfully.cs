using Brewup.Modules.Shared.Commands;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Warehouse.Core.Tests.Entities;

public class WithdrawalFromWarehouseSuccesfully : CommandSpecification<WithdrawalFromWarehouse>
{
	private readonly WarehouseId _warehouseId = new(Guid.NewGuid());
	private readonly WarehouseName _warehouseName = new("WarehouseName");

	private readonly BeerId _beerId = new(Guid.NewGuid().ToString());
	private readonly BeerName _beerName = new("Muflone IPA");
	private readonly MovementQuantity _movementQuantity = new(10);

	private readonly MovementId _movementId = new(Guid.NewGuid().ToString());
	private readonly MovementDate _movementDate = new(DateTime.Now);
	private readonly CausalId _causalId = new(Guid.NewGuid().ToString());
	private readonly CausalDescription _causalDescription = new("Versamento");

	private readonly IEnumerable<BeerDepositRow> _rows = Enumerable.Empty<BeerDepositRow>();
	private readonly IEnumerable<BeerAvailabilityUpdated> _availabilities = Enumerable.Empty<BeerAvailabilityUpdated>();

	private readonly IEnumerable<BeerToDrawn> _beers = Enumerable.Empty<BeerToDrawn>();
	private readonly IEnumerable<BeerToDrawn> _beersDrawn = Enumerable.Empty<BeerToDrawn>();

	private readonly Guid _commitId = Guid.NewGuid();

	public WithdrawalFromWarehouseSuccesfully()
	{
		_rows = _rows.Concat(new List<BeerDepositRow>
		{
			new(_beerId, _beerName, _movementQuantity)
		});

		_availabilities = _availabilities.Append(new BeerAvailabilityUpdated(_beerId, _beerName, _movementQuantity,
			new Stock(10), new Availability(10), new ProductionCommitted(0), new SalesCommitted(0),
			new SupplierOrdered(0)));

		_beers = _beers.Append(new BeerToDrawn(_beerId, new Quantity(5), new Stock(0), new Availability(0)));
		_beersDrawn = _beersDrawn.Concat(new List<BeerToDrawn>
		{
			new (_beerId, new Quantity(5), new Stock(5), new Availability(5))
		});
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new WarehouseCreated(_warehouseId, _warehouseName);
		yield return new BeerDepositAdded(_warehouseId, _movementId, _movementDate, _causalId, _causalDescription,
			_availabilities);
	}

	protected override WithdrawalFromWarehouse When()
	{
		return new WithdrawalFromWarehouse(_warehouseId, _commitId, _beers);
	}

	protected override ICommandHandlerAsync<WithdrawalFromWarehouse> OnHandler()
	{
		return new WithdrawalFromWarehouseCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeerWithdrawn(_warehouseId, _commitId, _beersDrawn);
	}
}