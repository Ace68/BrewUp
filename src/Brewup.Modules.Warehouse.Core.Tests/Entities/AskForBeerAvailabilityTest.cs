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

public sealed class AskForBeerAvailabilityTest : CommandSpecification<AskForBeersAvailability>
{
	private readonly WarehouseId _warehouseId = new(Guid.NewGuid());
	private readonly WarehouseName _warehouseName = new("MufloneStorage");

	private readonly MovementId _movementId = new(Guid.NewGuid().ToString());
	private readonly MovementDate _movementDate = new(DateTime.Now);
	private readonly CausalId _causalId = new(Guid.NewGuid().ToString());
	private readonly CausalDescription _causalDescription = new("Versamento");

	private readonly BeerId _beerId = new(Guid.NewGuid().ToString());
	private readonly BeerName _beerName = new("Muflone IPA");
	private readonly MovementQuantity _movementQuantity = new(10);

	private readonly IEnumerable<BeerDepositRow> _rows = Enumerable.Empty<BeerDepositRow>();

	private readonly IEnumerable<BeerAvailability> _beerAvailabilities = Enumerable.Empty<BeerAvailability>();
	private readonly IEnumerable<BeerAvailabilityUpdated> _availabilitiesUpdated = Enumerable.Empty<BeerAvailabilityUpdated>();

	private readonly Stock _stock = new(10);
	private readonly Availability _availability = new(10);
	private readonly ProductionCommitted _productionCommitted = new(0);
	private readonly SalesCommitted _salesCommitted = new(0);
	private readonly SupplierOrdered _supplierOrdered = new(0);

	private readonly Guid _correlationId = Guid.NewGuid();

	public AskForBeerAvailabilityTest()
	{
		_rows = _rows.Append(new BeerDepositRow(_beerId, _beerName, _movementQuantity));

		_beerAvailabilities = _beerAvailabilities.Concat(new List<BeerAvailability>
		{
			new (_beerId, _stock, _availability,
				_productionCommitted, _salesCommitted, _supplierOrdered)
		});

		_availabilitiesUpdated = _availabilitiesUpdated.Append(new BeerAvailabilityUpdated(_beerId, _beerName,
			_movementQuantity, _stock, _availability, _productionCommitted, _salesCommitted, _supplierOrdered));
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new WarehouseCreated(_warehouseId, _warehouseName);
		yield return new BeerDepositAdded(_warehouseId, _movementId, _movementDate, _causalId,
			_causalDescription, _availabilitiesUpdated);
	}

	protected override AskForBeersAvailability When() => new(_warehouseId, _correlationId, new List<BeerId> { _beerId });

	protected override ICommandHandlerAsync<AskForBeersAvailability> OnHandler()
	{
		return new AskForBeersAvailabilityCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeersAvailabilityAsked(_warehouseId, _correlationId, _beerAvailabilities);
	}
}