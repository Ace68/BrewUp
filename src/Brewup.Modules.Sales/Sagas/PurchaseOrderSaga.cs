using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Modules.Sales.Sagas;

public class PurchaseOrderSaga
{
	internal async Task StartAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken)
	{ }
}