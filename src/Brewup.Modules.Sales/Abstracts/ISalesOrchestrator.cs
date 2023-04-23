using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesOrchestrator
{
	Task<string> CreateSalesOrderAsync(SalesOrderJson orderToAdd, IEnumerable<BeerWithdrawn> beersWithdrawn,
		CancellationToken cancellationToken);
}