using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Sales.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Sales.Concretes;

public sealed class SalesOrchestrator : ISalesOrchestrator
{
	private readonly ILogger _logger;
	private readonly IServiceBus _serviceBus;

	public SalesOrchestrator(ILoggerFactory loggerFactory,
		IServiceBus serviceBus)
	{
		_serviceBus = serviceBus;
		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> AddOrderAsync(SalesOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		try
		{
			if (string.IsNullOrEmpty(orderToAdd.OrderId))
				orderToAdd.OrderId = Guid.NewGuid().ToString();

			// Verify that BeerId, CustomerId, etc. are valid

			//Start Saga here!
			var launchSalesOrder = new LaunchSalesOrderSaga(
								new OrderId(new Guid(orderToAdd.OrderId)),
												Guid.NewGuid(),
												new OrderNumber(orderToAdd.OrderNumber),
												new OrderDate(orderToAdd.OrderDate),
												new CustomerId(orderToAdd.CustomerId),
												new CustomerName(orderToAdd.CustomerName),
												new TotalAmount(orderToAdd.TotalAmount),
												orderToAdd.Rows.Select(r => new PurchaseOrderRow(
													new OrderId(new Guid(orderToAdd.OrderId)),
													new RowId(Guid.NewGuid().ToString()),
													new RowNumber(r.RowNumber),
													new BeerId(r.BeerId),
													new BeerName(r.BeerName),
													new QuantityOrdered(r.QuantityOrdered),
													new QuantityDelivered(r.QuantityDelivered),
													new UnitPrice(r.UnitPrice),
													new TotalAmount(r.UnitPrice * r.QuantityOrdered))));

			await _serviceBus.SendAsync(launchSalesOrder, cancellationToken);

			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}