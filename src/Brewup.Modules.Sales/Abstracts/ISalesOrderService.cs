using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Modules.Sales.Abstracts;

public interface ISalesOrderService
{
	Task AddOrderAsync(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate, CustomerId customerId,
		CustomerName customerName, TotalAmount totalAmount, CancellationToken cancellationToken);
}