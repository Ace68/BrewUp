using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Dtos;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Purchases.Concretes;

public sealed class PurchaseOrchestrator : IPurchaseOrchestrator
{
	private readonly PurchaseLogger _purchaseLogger;

	public PurchaseOrchestrator(ILoggerFactory loggerFactory)
	{
		_purchaseLogger = new PurchaseLogger(loggerFactory);
	}

	public Task<string> AddOrderAsync(PurchaseOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		try
		{
			throw new NotImplementedException();
		}
		catch (Exception ex)
		{
			_purchaseLogger.Logger.LogError("");
			throw;
		}
	}
}