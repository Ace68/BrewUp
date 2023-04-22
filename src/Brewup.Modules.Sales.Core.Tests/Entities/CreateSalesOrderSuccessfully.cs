using Brewup.Modules.Sales.Core.CommandHandlers;
using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.CustomTypes;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Modules.Shared.CustomTypes;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Sales.Core.Tests.Entities;

public sealed class CreateSalesOrderSuccessfully : CommandSpecification<CreateSalesOrder>
{
	private readonly OrderId _orderId = new(Guid.NewGuid());
	private readonly OrderNumber _orderNumber = new("20230420-01");
	private readonly OrderDate _orderDate = new(DateTime.UtcNow);

	private readonly CustomerId _customerId = new(Guid.NewGuid().ToString());
	private readonly CustomerName _customerName = new("Muflone");

	private readonly TotalAmount _totalAmount = new(100);

	private IEnumerable<SalesOrderRow> _rows = Enumerable.Empty<SalesOrderRow>();

	private readonly RowId _rowId = new(Guid.NewGuid().ToString());
	private readonly BeerId _beerId = new(Guid.NewGuid().ToString());
	private readonly BeerName _beerName = new("Birra del Muflone");
	private readonly QuantityOrdered _quantityOrdered = new(10);
	private readonly QuantityDelivered _quantityDelivered = new(0);
	private readonly UnitPrice _unitPrice = new(10);

	private readonly Guid _commitId = Guid.NewGuid();

	public CreateSalesOrderSuccessfully()
	{
		_rows = _rows.Concat(new List<SalesOrderRow>
		{
			new(_rowId, _beerId, _beerName, _quantityOrdered, _quantityDelivered, _unitPrice)
		});
	}

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateSalesOrder When()
	{
		return new CreateSalesOrder(_orderId, _commitId, _orderNumber, _orderDate, _customerId, _customerName, _totalAmount, _rows);
	}

	protected override ICommandHandlerAsync<CreateSalesOrder> OnHandler()
	{
		return new CreateSalesOrderCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new SalesOrderCreated(_orderId, _commitId, _orderNumber, _orderDate, _customerId, _customerName,
			_totalAmount, _rows);
	}
}