using Brewup.Infrastructure.Proxy.Abstracts;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Infrastructure.Proxy.Concretes;

public sealed class PurchaseOrderSaga : IPurchaseOrderSaga
{
	private readonly BrewupProxy _brewupProxy;

	public PurchaseOrderSaga(BrewupProxy brewupProxy)
	{
		_brewupProxy = brewupProxy;
	}

	public async Task StartAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		var beersWithdrawn = await _brewupProxy.WithdrawalBeerFromWarehouseAsync(
						new WarehouseId(new Guid(orderToAdd.WarehouseId)),
						orderToAdd.Rows.Select(r => new BeerToDrawn(new BeerId(r.BeerId), new Quantity(r.QuantityOrdered), new Stock(0), new Availability(0))),
						cancellationToken);
	}
}