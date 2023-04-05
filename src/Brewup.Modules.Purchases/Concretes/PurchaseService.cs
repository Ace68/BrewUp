using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Dtos;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Purchases.Concretes;

public sealed class PurchaseService : IPurchaseService
{
	private PurchaseBaseService BaseService { get; }

	public PurchaseService(ILoggerFactory loggerFactory)
	{
		BaseService = new PurchaseBaseService(loggerFactory);
	}

	public Task<string> AddOrderAsync(PurchaseOrder orderToAdd, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}