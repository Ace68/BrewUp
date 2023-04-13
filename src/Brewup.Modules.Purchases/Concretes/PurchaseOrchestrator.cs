using Brewup.Modules.Purchases.Abstracts;
using Brewup.Modules.Purchases.Shared.Commands;
using Brewup.Modules.Purchases.Shared.Dtos;
using Brewup.Modules.Purchases.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Purchases.Concretes;

public sealed class PurchaseOrchestrator : IPurchaseOrchestrator
{
	private readonly ILogger _logger;
	private readonly IServiceBus _serviceBus;

	public PurchaseOrchestrator(ILoggerFactory loggerFactory,
		IServiceBus serviceBus)
	{
		_serviceBus = serviceBus;
		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> AddOrderAsync(PurchaseOrderJson orderToAdd, CancellationToken cancellationToken)
	{
		try
		{
			if (string.IsNullOrEmpty(orderToAdd.OrderId))
				orderToAdd.OrderId = Guid.NewGuid().ToString();

			var createPurchaseOrder = new CreatePurchaseOrder(
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
													new BeerId(new Guid(r.BeerId)),
													new BeerName(r.BeerName),
													new QuantityOrdered(r.QuantityOrdered),
													new QuantityDelivered(r.QuantityDelivered),
													new UnitPrice(r.UnitPrice),
													new TotalAmount(r.UnitPrice * r.QuantityOrdered))));

			await _serviceBus.SendAsync(createPurchaseOrder, cancellationToken);

			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}