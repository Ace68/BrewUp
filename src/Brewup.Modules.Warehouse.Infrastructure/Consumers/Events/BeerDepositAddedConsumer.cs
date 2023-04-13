using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Events;

public sealed class BeerDepositAddedConsumer : DomainEventConsumerBase<BeerDepositAdded>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerDepositAdded>> HandlersAsync { get; }

	public BeerDepositAddedConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<BeerDepositAdded>>();
	}
}