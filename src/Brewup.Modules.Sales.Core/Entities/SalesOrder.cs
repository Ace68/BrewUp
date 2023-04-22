using Brewup.Modules.Shared.CustomTypes;
using Muflone.Core;

namespace Brewup.Modules.Sales.Core.Entities;

public sealed class SalesOrder : AggregateRoot
{
	private OrderId _orderId;
	private OrderNumber _orderNumber;
	private OrderDate _orderDate;

	private CustomerId _customerId;
	private CustomerName _customerName;

	private TotalAmount _totalAmount;

	private IEnumerable<OrderRow> _rows;

	protected SalesOrder()
	{ }

	#region ctor

	internal static SalesOrder CreateSalesOrder(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate,
		CustomerId customerId,
		CustomerName customerName, TotalAmount totalAmount, IEnumerable<OrderRow> rows)
	{
		var salesOrder = new SalesOrder
		{
			_orderId = orderId,
			_orderNumber = orderNumber,
			_orderDate = orderDate,
			_customerId = customerId,
			_customerName = customerName,
			_totalAmount = totalAmount,
			_rows = rows
		};
		return salesOrder;
	}

	private SalesOrder(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate, CustomerId customerId,
		CustomerName customerName, TotalAmount totalAmount, IEnumerable<OrderRow> rows)
	{
		_orderId = orderId;
		_orderNumber = orderNumber;
		_orderDate = orderDate;
		_customerId = customerId;
		_customerName = customerName;
		_totalAmount = totalAmount;
		_rows = rows;
	}
	#endregion
}