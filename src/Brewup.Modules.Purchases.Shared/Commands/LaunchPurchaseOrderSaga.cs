using Brewup.Modules.Purchases.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Purchases.Shared.Commands;

public sealed class LaunchPurchaseOrderSaga : Command
{
	public readonly OrderId OrderId;
	public readonly OrderNumber OrderNumber;
	public readonly OrderDate OrderDate;

	public readonly CustomerId CustomerId;
	public readonly CustomerName CustomerName;

	public readonly TotalAmount TotalAmount;

	public readonly IEnumerable<PurchaseOrderRow> Rows;

	public LaunchPurchaseOrderSaga(OrderId aggregateId, Guid commitId, OrderNumber orderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, TotalAmount totalAmount, IEnumerable<PurchaseOrderRow> rows) :
		base(aggregateId, commitId)
	{
		OrderId = aggregateId;
		OrderNumber = orderNumber;
		OrderDate = orderDate;

		CustomerId = customerId;
		CustomerName = customerName;

		TotalAmount = totalAmount;

		Rows = rows;
	}
}