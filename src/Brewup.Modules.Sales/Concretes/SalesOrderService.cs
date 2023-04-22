using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrderService : PurchaseBaseService, ISalesOrderService
{
	public SalesOrderService(IPersister persister,
		ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{
	}

	public async Task AddOrderAsync(SalesOrderCreated @event, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrder = await Persister.GetByIdAsync<SalesOrder>(@event.OrderId.Value.ToString());
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}