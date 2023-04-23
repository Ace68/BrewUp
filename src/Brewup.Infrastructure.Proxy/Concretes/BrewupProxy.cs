using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Abstracts;

namespace Brewup.Infrastructure.Proxy.Concretes;

public class BrewupProxy
{
	private readonly ISalesOrchestrator _salesOrchestrator;
	private readonly IWarehouseOrchestrator _warehouseOrchestrator;

	public BrewupProxy(ISalesOrchestrator salesOrchestrator, IWarehouseOrchestrator warehouseOrchestrator)
	{
		_salesOrchestrator = salesOrchestrator ?? throw new ArgumentNullException(nameof(salesOrchestrator));
		_warehouseOrchestrator = warehouseOrchestrator ?? throw new ArgumentNullException(nameof(warehouseOrchestrator));
	}

	internal async Task<IEnumerable<BeerWithdrawn>> WithdrawalBeerFromWarehouseAsync(WarehouseId warehouseId,
		IEnumerable<BeerToDrawn> beersToDrawn, CancellationToken cancellationToken) =>
		await _warehouseOrchestrator.WithdrawalBeerFromWarehouseAsync(warehouseId, beersToDrawn, cancellationToken);
}