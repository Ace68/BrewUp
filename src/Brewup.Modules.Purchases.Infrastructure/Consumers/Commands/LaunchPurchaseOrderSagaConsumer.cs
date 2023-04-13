using Brewup.Modules.Purchases.Sagas;
using Brewup.Modules.Purchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Purchases.Infrastructure.Consumers.Commands;

public sealed class LaunchPurchaseOrderSagaConsumer : CommandConsumerBase<LaunchPurchaseOrderSaga>
{
	protected override ICommandHandlerAsync<LaunchPurchaseOrderSaga> HandlerAsync { get; }

	public LaunchPurchaseOrderSagaConsumer(ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new PurchaseOrderSaga();
	}
}