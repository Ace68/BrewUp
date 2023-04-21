using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Shared.Commands;
using Brewup.Modules.Shared.DomainEvents;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace Brewup.Modules.Sales.Sagas;

public class PurchaseOrderSaga : Saga<SalesSagaState>,
	ISagaStartedByAsync<LaunchSalesOrderSaga>,
	IDomainEventHandlerAsync<BeersAvailabilityAsked>
{
	public PurchaseOrderSaga(IServiceBus serviceBus,
		ISagaRepository repository,
		ILoggerFactory loggerFactory) : base(serviceBus, repository, loggerFactory)
	{
	}

	public async Task StartedByAsync(LaunchSalesOrderSaga command)
	{
		SagaState = new SalesSagaState
		{
			Order = new SalesOrderJson
			{
				OrderId = command.OrderId.Value.ToString(),
				OrderNumber = command.OrderNumber.Value,
				OrderDate = command.OrderDate.Value,

				WarehouseId = command.WarehouseId.Value.ToString(),

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

		var correlationId = Guid.NewGuid();
		await Repository.SaveAsync(correlationId, SagaState);

		// I have to send the first command to start the saga
		var askForBeersAvailability =
			new AskForBeersAvailability(command.WarehouseId, correlationId, command.Rows.Select(r => r.BeerId));
		await ServiceBus.SendAsync(askForBeersAvailability, CancellationToken.None);
	}

	public async Task HandleAsync(BeersAvailabilityAsked @event, CancellationToken cancellationToken = new())
	{
		var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString());
		var sagaState = await Repository.GetByIdAsync<SalesSagaState>(correlationId);


	}
}