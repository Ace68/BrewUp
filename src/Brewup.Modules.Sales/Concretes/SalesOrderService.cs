using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Infrastructure.ReadModel.Models;
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
			var salesOrder = await Persister.GetByIdAsync<SalesOrder>(@event.OrderId.Value.ToString(), cancellationToken);
			if (!string.IsNullOrWhiteSpace(salesOrder.OrderId))
				return;

			salesOrder = SalesOrder.CreateSalesOrder(@event.OrderId, @event.OrderNumber, @event.OrderDate, @event.CustomerId,
								@event.CustomerName, @event.TotalAmount);
			await Persister.InsertAsync(salesOrder, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}