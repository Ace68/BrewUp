using Brewup.Modules.Sales.Sagas;
using Brewup.Modules.Sales.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Commands;

public sealed class LaunchSalesOrderSagaConsumer : CommandConsumerBase<LaunchSalesOrderSaga>
{
	protected override ICommandHandlerAsync<LaunchSalesOrderSaga> HandlerAsync { get; }

	public LaunchSalesOrderSagaConsumer(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new PurchaseOrderSaga();
	}
}