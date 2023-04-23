using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrderService : PurchaseBaseService, ISalesOrderService
{
	public SalesOrderService(IPersister persister,
		ILoggerFactory loggerFactory) : base(persister, loggerFactory)
	{
	}

	public async Task AddOrderAsync(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, TotalAmount totalAmount, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrder = await Persister.GetByIdAsync<SalesOrder>(orderId.Value.ToString(), cancellationToken);
			if (!string.IsNullOrWhiteSpace(salesOrder.OrderId))
				return;

			salesOrder = SalesOrder.CreateSalesOrder(orderId, orderNumber, orderDate, customerId,
								customerName, totalAmount);
			await Persister.InsertAsync(salesOrder, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}