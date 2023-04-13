using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.Dtos;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace Brewup.Modules.Sales.Sagas;

public class PurchaseOrderSaga2 : Saga<SalesSagaState>
{
	public PurchaseOrderSaga2(IServiceBus serviceBus, ISagaRepository repository) : base(serviceBus, repository)
	{
	}
}

public class PurchaseOrderSaga : ISaga<SalesSagaState>,
	ICommandHandlerAsync<LaunchSalesOrderSaga>
{
	public SalesSagaState SagaState { get; set; } = new();

	public Task HandleAsync(LaunchSalesOrderSaga command, CancellationToken cancellationToken = new())
	{
		SagaState = new SalesSagaState
		{
			Order = new SalesOrderJson
			{
				OrderId = command.OrderId.Value.ToString(),
				OrderNumber = command.OrderNumber.Value,
				OrderDate = command.OrderDate.Value,
				CustomerId = command.CustomerId.Value,
				CustomerName = command.CustomerName.Value,
				TotalAmount = command.TotalAmount.Value,
				Rows = command.Rows.Select(r => new SalesOrderRowJson
				{
					RowNumber = r.RowNumber.Value,
					BeerId = r.BeerId.Value.ToString(),
					BeerName = r.BeerName.Value,
					QuantityOrdered = r.QuantityOrdered.Value,
					QuantityDelivered = r.QuantityDelivered.Value,
					UnitPrice = r.UnitPrice.Value
				}).ToList()
			}
		};

		// I have to send the first command to start the saga

		// Check Beer Availability

		return Task.CompletedTask;
	}

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~PurchaseOrderSaga()
	{
		Dispose(false);
	}
	#endregion
}