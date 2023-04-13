using Brewup.Modules.Purchases.Shared.Dtos;

namespace Brewup.Modules.Purchases.Abstracts;

public interface IPurchaseService
{
	Task<string> AddOrderAsync(PurchaseOrderJson orderToAdd, CancellationToken cancellationToken);
}