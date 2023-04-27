using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesOrderService
{
	Task AddOrderAsync(SalesOrderCreated @event, CancellationToken cancellationToken);
	Task<IEnumerable<SalesOrderJson>> GetSalesOrdersAsync(CancellationToken cancellationToken);
}