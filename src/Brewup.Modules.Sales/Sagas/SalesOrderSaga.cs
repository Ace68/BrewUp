using Brewup.Modules.Sales.Shared.Commands;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Brewup.Modules.Sales.Shared.Dtos;
using Brewup.Modules.Sales.Shared.Helpers;
using Brewup.Modules.Shared.Commands;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Shared.DomainEvents;
using Brewup.Modules.Shared.IntegrationEvents;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Saga;
using Muflone.Saga.Persistence;

namespace Brewup.Modules.Sales.Sagas;

public class SalesOrderSaga : Saga<SalesSagaState>,
	ISagaStartedByAsync<LaunchSalesOrderSaga>,
	IDomainEventHandlerAsync<BeersAvailabilityAsked>,
	IIntegrationEventHandlerAsync<BroadcastBeerWithdrawn>,
	IDomainEventHandlerAsync<SalesOrderCreated>
{
	public SalesOrderSaga(IServiceBus serviceBus,
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

		// Save SagaState
		var correlationId = Guid.NewGuid();
		await Repository.SaveAsync(correlationId, SagaState);

		// I have to send the first command of the saga
		var askForBeersAvailability =
			new AskForBeersAvailability(command.WarehouseId, correlationId, command.Rows.Select(r => r.BeerId));
		await ServiceBus.SendAsync(askForBeersAvailability, CancellationToken.None);
	}

	public async Task HandleAsync(BeersAvailabilityAsked @event, CancellationToken cancellationToken = new())
	{
		var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		var sagaState = await Repository.GetByIdAsync<SalesSagaState>(correlationId);

		if (sagaState is null)
			return;

		// Verify availability
		// With availability we create order
		// Without availability we send a message to the customer

		var withdrawalFromWarehouse = new WithdrawalFromWarehouse(@event.WarehouseId, correlationId,
			sagaState.Order.Rows.Select(r => r.ToBeerToDrawn()));

		await ServiceBus.SendAsync(withdrawalFromWarehouse);
	}

	public async Task HandleAsync(BroadcastBeerWithdrawn @event, CancellationToken cancellationToken = new())
	{
		var correlationId = new Guid(@event.UserProperties.FirstOrDefault(u => u.Key.Equals("CorrelationId")).Value.ToString()!);
		var sagaState = await Repository.GetByIdAsync<SalesSagaState>(correlationId);

		if (sagaState is null)
			return;

		var createSalesOrder = new CreateSalesOrder(
			new OrderId(new Guid(sagaState.Order.OrderId)),
			correlationId,
			new OrderNumber(sagaState.Order.OrderNumber),
			new OrderDate(sagaState.Order.OrderDate),
			new CustomerId(sagaState.Order.CustomerId),
			new CustomerName(sagaState.Order.CustomerName),
			new TotalAmount(sagaState.Order.TotalAmount),
			sagaState.Order.Rows.Select(r => r.ToSalesOrderRow()));

		await ServiceBus.SendAsync(createSalesOrder, cancellationToken);
	}

	public Task HandleAsync(SalesOrderCreated @event, CancellationToken cancellationToken = new())
	{
		return Task.CompletedTask;
	}
}