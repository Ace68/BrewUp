using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.EventsHandler;

public class WarehouseCreatedEventHandler : DomainEventHandlerAsync<WarehouseCreated>
{
	private readonly IWarehouseService _warehouseService;

	public WarehouseCreatedEventHandler(ILoggerFactory loggerFactory,
		IWarehouseService warehouseService) : base(loggerFactory)
	{
		_warehouseService = warehouseService;
	}

	public override async Task HandleAsync(WarehouseCreated @event, CancellationToken cancellationToken = new())
	{
		try
		{
			await _warehouseService.CreateWarehouseAsync(@event.WarehouseId, @event.WarehouseName, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}