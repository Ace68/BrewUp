using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Warehouse.Core.Tests.Entities;

public sealed class CreateWarehouseSuccesfully : CommandSpecification<CreateWarehouse>
{
	private readonly WarehouseId _warehouseId = new(Guid.NewGuid());
	private readonly WarehouseName _warehouseName = new("WarehouseName");

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateWarehouse When()
	{
		return new CreateWarehouse(_warehouseId, _warehouseName);
	}

	protected override ICommandHandlerAsync<CreateWarehouse> OnHandler()
	{
		return new CreateWarehouseCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new WarehouseCreated(_warehouseId, _warehouseName);
	}
}