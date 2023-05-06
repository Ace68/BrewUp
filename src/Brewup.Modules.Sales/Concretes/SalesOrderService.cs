using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Concretes;

internal sealed class SalesOrderService : SalesBaseService, ISalesOrderService
{
	public SalesOrderService(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
	{
	}

	public async Task AddOrderAsync(SalesOrderCreated @event, CancellationToken cancellationToken)
	{
		try
		{
			var salesOrder = await Persister.GetByIdAsync<SalesOrder>(@event.OrderId.Value.ToString(), cancellationToken);
			if (!string.IsNullOrWhiteSpace(salesOrder.OrderId))
				return;

			salesOrder = SalesOrder.CreateSalesOrder(@event.OrderId, @event.OrderNumber, @event.OrderDate, @event.CustomerId,
								@event.CustomerName, @event.TotalAmount);
			await Persister.InsertAsync(salesOrder, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task<IEnumerable<SalesOrderJson>> GetSalesOrdersAsync(CancellationToken cancellationToken)
	{
		try
		{
			var orders = await Persister.FindAsync<SalesOrder>();
			var ordersArray = orders as SalesOrder[] ?? orders.ToArray();

			return ordersArray.Any()
				? ordersArray.Select(o => o.ToJson())
				: Enumerable.Empty<SalesOrderJson>();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}