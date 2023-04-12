using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Stores.Core.Tests.Entities;

public sealed class AskForBeerAvailabilityTest : CommandSpecification<AskForBeerAvailability>
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
		yield return new BeerDepositAdded(_beerId, _storeId, _movementId, _movementDate, _movementQuantity, _causalId,
			_causalDescription, _stock, _availability, _salesCommitted, _productionCommitted, _supplierOrdered);
	}

	protected override AskForBeerAvailability When() => new(_beerId);

	protected override ICommandHandlerAsync<AskForBeerAvailability> OnHandler()
	{
		return new AskForBeerAvailabilityCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeerAvailabilityChecked(_beerId, _stock, _availability, _productionCommitted, _salesCommitted,
			_supplierOrdered);
	}
}