using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesService
{
	Task<string> AddOrderAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken);
}