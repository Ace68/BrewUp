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





			return orderToAdd.OrderId;
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}