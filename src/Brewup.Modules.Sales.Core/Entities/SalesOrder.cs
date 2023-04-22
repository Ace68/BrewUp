using Brewup.Modules.Sales.Core.Helpers;
using Brewup.Modules.Sales.Shared.CustomTypes;
using Brewup.Modules.Sales.Shared.DomainEvents;
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
		CustomerId customerId, CustomerName customerName, TotalAmount totalAmount, IEnumerable<SalesOrderRow> rows,
		Guid commitId)
	{
		var salesOrder = new SalesOrder(orderId, orderNumber, orderDate, customerId, customerName, totalAmount, rows,
						commitId);

		return salesOrder;
	}

	private SalesOrder(OrderId orderId, OrderNumber orderNumber, OrderDate orderDate, CustomerId customerId,
		CustomerName customerName, TotalAmount totalAmount, IEnumerable<SalesOrderRow> rows, Guid commitId)
	{
		RaiseEvent(new SalesOrderCreated(orderId, commitId, orderNumber, orderDate, customerId, customerName,
			totalAmount, rows));
	}

	private void Apply(SalesOrderCreated @event)
	{
		Id = @event.AggregateId;

		_orderId = @event.OrderId;
		_orderNumber = @event.OrderNumber;
		_orderDate = @event.OrderDate;
		_customerId = @event.CustomerId;
		_customerName = @event.CustomerName;
		_totalAmount = @event.TotalAmount;
		_rows = @event.Rows.Select(r => r.ToEntity());
	}
	#endregion
}