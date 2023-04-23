using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Infrastructure.Proxy.Abstracts;

public interface IPurchaseOrderSaga
{
	Task StartAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken);
}