using Brewup.Modules.Sales.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Events;

public sealed class SalesOrderCreatedConsumer : DomainEventConsumerBase<SalesOrderCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderCreated>> HandlersAsync { get; }

	public SalesOrderCreatedConsumer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<SalesOrderCreated>>().ToArray();
	}
}