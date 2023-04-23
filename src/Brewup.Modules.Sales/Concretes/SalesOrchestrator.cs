using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrchestrator : ISalesOrchestrator
{
	private readonly ILogger _logger;


	public SalesOrchestrator(ILoggerFactory loggerFactory)
	{
		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> CreateSalesOrderAsync(SalesOrderJson orderToAdd, IEnumerable<BeerWithdrawn> beersWithdrawn,
		CancellationToken cancellationToken)
	{
		try
		{
			if (string.IsNullOrEmpty(orderToAdd.OrderId))
				orderToAdd.OrderId = Guid.NewGuid().ToString();

			// Verify that BeerId, CustomerId, etc. are valid

			// Start Saga here!


			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}