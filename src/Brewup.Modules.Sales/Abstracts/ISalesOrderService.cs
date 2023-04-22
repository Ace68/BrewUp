using Brewup.Modules.Sales.Shared.DomainEvents;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesOrderService
{
	Task AddOrderAsync(SalesOrderCreated @event, CancellationToken cancellationToken);
}