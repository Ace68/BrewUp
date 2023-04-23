using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Warehouse.Core.Tests.Entities;

public sealed class AddBeerDepositTest : CommandSpecification<AddBeerDeposit>
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

	public AddBeerDepositTest()
	{
		_rows = _rows.Concat(new List<BeerDepositRow>
		{
			new(_beerId, _beerName, _movementQuantity)
		});

		_availabilities = _availabilities.Concat(new List<BeerAvailabilityUpdated>
		{
			new(_beerId, _beerName, _movementQuantity, new Stock(10), new Availability(10), new ProductionCommitted(0),
								new SalesCommitted(0), new SupplierOrdered(0))
		});
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new WarehouseCreated(_warehouseId, _warehouseName);
	}

	protected override AddBeerDeposit When() => new(_warehouseId, _movementId, _movementDate, _causalId,
		_causalDescription, _rows);

	protected override ICommandHandlerAsync<AddBeerDeposit> OnHandler()
	{
		return new AddBeerDepositCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeerDepositAdded(_warehouseId, _movementId, _movementDate, _causalId, _causalDescription,
			_availabilities);
	}
}