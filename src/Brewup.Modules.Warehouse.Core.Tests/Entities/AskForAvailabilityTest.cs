using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Warehouse.Core.Tests.Entities;

public class AskForAvailabilityTest : CommandSpecification<AskForAvailability>
{
	private readonly SpareId _spareId = new(Guid.NewGuid());
	private readonly Stock _stock = new(10);
	private readonly Availability _availability = new(10);
	private readonly ProductionCommitted _productionCommitted = new(0);
	private readonly SalesCommitted _salesCommitted = new(0);
	private readonly SupplierOrdered _supplierOrdered = new(0);

	protected override IEnumerable<DomainEvent> Given()
	{
		yield return new SparesAvailabilityCreated(_spareId, _stock, _availability, _productionCommitted, _salesCommitted, _supplierOrdered);
	}

	protected override AskForAvailability When()
	{
		return new AskForAvailability(_spareId);
	}

	protected override ICommandHandlerAsync<AskForAvailability> OnHandler()
	{
		return new AskForAvailabilityCommandHandler(Repository, new LoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new AvailabilityChecked(_spareId, _availability);
	}
}