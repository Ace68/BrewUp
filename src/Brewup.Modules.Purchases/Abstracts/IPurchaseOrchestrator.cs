using Brewup.Modules.Purchases.Shared.Dtos;

namespace Brewup.Modules.Purchases.Abstracts;

public interface IPurchaseOrchestrator
{
	Task<string> AddOrderAsync(PurchaseOrderJson orderToAdd, CancellationToken cancellationToken);
}