using Brewup.Modules.Purchases.Shared.Commands;
using Brewup.Modules.Purchases.Shared.Dtos;
using Muflone.Messages.Commands;
using Muflone.Saga;

namespace Brewup.Modules.Purchases.Sagas;

public class PurchaseOrderSaga : ISaga<PurchaseSagaState>,
	ICommandHandlerAsync<LaunchPurchaseOrderSaga>
{
	public PurchaseSagaState SagaState { get; set; } = new();

	public Task HandleAsync(LaunchPurchaseOrderSaga command, CancellationToken cancellationToken = new())
	{
		SagaState = new PurchaseSagaState
		{
			Order = new PurchaseOrderJson
			{
				OrderId = command.OrderId.Value.ToString(),
				OrderNumber = command.OrderNumber.Value,
				OrderDate = command.OrderDate.Value,
				CustomerId = command.CustomerId.Value,
				CustomerName = command.CustomerName.Value,
				TotalAmount = command.TotalAmount.Value,
				Rows = command.Rows.Select(r => new PurchaseOrderRowJson
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