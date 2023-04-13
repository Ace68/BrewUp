using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

public sealed class SalesService : PurchaseBaseService, ISalesService
{
	public SalesService(IPersister persister,
		ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{

	}

	public Task<string> AddOrderAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}