using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Sagas;
using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrchestrator : ISalesOrchestrator
{
	private readonly ILogger _logger;
	private readonly SalesOrderSaga _purchaseOrderSaga;
	private readonly ISalesOrderService _salesOrderService;

	public SalesOrchestrator(ILoggerFactory loggerFactory,
		SalesOrderSaga purchaseOrderSaga,
		ISalesOrderService salesOrderService)
	{
		_purchaseOrderSaga = purchaseOrderSaga ?? throw new ArgumentNullException(nameof(purchaseOrderSaga));
		_salesOrderService = salesOrderService ?? throw new ArgumentNullException(nameof(salesOrderService));
		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> AddOrderAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		try
		{
			if (string.IsNullOrEmpty(orderToAdd.OrderId))
				orderToAdd.OrderId = Guid.NewGuid().ToString();

			// Verify that BeerId, CustomerId, etc. are valid

			// Start Saga here!
			var launchSalesOrder = new LaunchSalesOrderSaga(new OrderId(new Guid(orderToAdd.OrderId)), Guid.NewGuid(),
				new OrderNumber(orderToAdd.OrderNumber), new OrderDate(orderToAdd.OrderDate),
				new WarehouseId(new Guid(orderToAdd.WarehouseId)), new CustomerId(orderToAdd.CustomerId),
				new CustomerName(orderToAdd.CustomerName), new TotalAmount(orderToAdd.TotalAmount),
				orderToAdd.Rows.Select(r => new PurchaseOrderRow(new OrderId(new Guid(orderToAdd.OrderId)),
					new RowId(Guid.NewGuid().ToString()), new RowNumber(r.RowNumber), new BeerId(r.BeerId),
					new BeerName(r.BeerName), new QuantityOrdered(r.QuantityOrdered),
					new QuantityDelivered(r.QuantityDelivered), new UnitPrice(r.UnitPrice),
					new TotalAmount(r.UnitPrice * r.QuantityOrdered))));

			await _purchaseOrderSaga.StartedByAsync(launchSalesOrder);

			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public Task<IEnumerable<SalesOrderJson>> GetSalesOrdersAsync(CancellationToken cancellationToken) =>
		_salesOrderService.GetSalesOrdersAsync(cancellationToken);
}