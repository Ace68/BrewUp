using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Infrastructure.ReadModel.Models;

public class SalesOrder : ModelBase
{
	public string OrderId { get; private set; }
	public string OrderNumber { get; private set; }
	public DateTime OrderDate { get; private set; }

	public string CustomerId { get; private set; }
	public string CustomerName { get; private set; }

	public double TotalAmount { get; private set; }


	protected SalesOrder()
	{ }

	internal static SalesOrder CreateSalesOrder(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate,
		CustomerId customerId, CustomerName customerName, TotalAmount totalAmount)
	{
		var salesOrder = new SalesOrder(orderId.Value.ToString(), orderNumber.Value, orderDate.Value, customerId.Value,
			customerName.Value, totalAmount.Value);

		return salesOrder;
	}

	private SalesOrder(string orderId, string orderNumber, DateTime orderDate, string customerId, string customerName, double totalAmount)
	{
		OrderId = orderId;
		OrderNumber = orderNumber;
		OrderDate = orderDate;
		CustomerId = customerId;
		CustomerName = customerName;
		TotalAmount = totalAmount;
	}
}