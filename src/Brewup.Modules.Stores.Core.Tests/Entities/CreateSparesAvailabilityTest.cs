using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Stores.Core.Tests.Entities;

public sealed class CreateSparesAvailabilityTest : CommandSpecification<CreateSpareAvailability>
{
	private readonly SpareId _spareId = new(Guid.NewGuid());
	private readonly Stock _stock = new(10);
	private readonly Availability _availability = new(10);
	private readonly ProductionCommitted _productionCommitted = new(0);
	private readonly SalesCommitted _salesCommitted = new(0);
	private readonly SupplierOrdered _supplierOrdered = new(0);

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateSpareAvailability When()
	{
		return new CreateSpareAvailability(_spareId, _stock, _availability, _productionCommitted, _salesCommitted,
			_supplierOrdered);
	}

	protected override ICommandHandlerAsync<CreateSpareAvailability> OnHandler()
	{
		return new CreateSpareAvailabilityCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new SparesAvailabilityCreated(_spareId, _stock, _availability, _productionCommitted, _salesCommitted, _supplierOrdered);
	}
}