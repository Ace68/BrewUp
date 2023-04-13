using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Warehouse.Core.Tests.Entities;

public sealed class AddBeerDepositTest : CommandSpecification<AddBeerDeposit>
{
	private readonly BeerId _beerId = new(Guid.NewGuid());
	private readonly BeerName _beerName = new("Muflone IPA");

	private readonly StoreId _storeId = new(Guid.NewGuid());
	private readonly MovementId _movementId = new(Guid.NewGuid().ToString());
	private readonly MovementDate _movementDate = new(DateTime.Now);
	private readonly CausalId _causalId = new(Guid.NewGuid().ToString());
	private readonly CausalDescription _causalDescription = new("Versamento");
	private readonly MovementQuantity _movementQuantity = new(10);

	private readonly Stock _stock = new(10);
	private readonly Availability _availability = new(10);
	private readonly ProductionCommitted _productionCommitted = new(0);
	private readonly SalesCommitted _salesCommitted = new(0);
	private readonly SupplierOrdered _supplierOrdered = new(0);

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new BeerCreated(_beerId, _beerName);
	}

	protected override AddBeerDeposit When() => new(_beerId, _storeId, _movementId, _movementDate, _causalId,
		_causalDescription, _movementQuantity);

	protected override ICommandHandlerAsync<AddBeerDeposit> OnHandler()
	{
		return new AddBeerDepositCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeerDepositAdded(_beerId, _storeId, _movementId, _movementDate, _movementQuantity, _causalId,
			_causalDescription, _stock, _availability, _salesCommitted, _productionCommitted, _supplierOrdered);
	}
}