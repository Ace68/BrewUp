using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesOrchestrator
{
	Task<string> AddOrderAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken);
}