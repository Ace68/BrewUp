using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Sagas;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrchestrator : ISalesOrchestrator
{
	private readonly ILogger _logger;
	private readonly PurchaseOrderSaga _purchaseOrderSaga;


	public SalesOrchestrator(ILoggerFactory loggerFactory,
		PurchaseOrderSaga purchaseOrderSaga)
	{
		_purchaseOrderSaga = purchaseOrderSaga ?? throw new ArgumentNullException(nameof(purchaseOrderSaga));
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
			await _purchaseOrderSaga.StartAsync(orderToAdd, cancellationToken);

			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}