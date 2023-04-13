using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Shared.Dtos;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Purchases.Concretes;

public sealed class PurchaseService : PurchaseBaseService, IPurchaseService
{
	public PurchaseService(IPersister persister,
		ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{

	}

	public Task<string> AddOrderAsync(PurchaseOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}