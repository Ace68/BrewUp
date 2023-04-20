using Brewup.Modules.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Events;

public sealed class BeersAvailabilityAskedConsumer : DomainEventConsumerBase<BeersAvailabilityAsked>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeersAvailabilityAsked>> HandlersAsync { get; }

	public BeersAvailabilityAskedConsumer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory,
		ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<BeersAvailabilityAsked>>();
	}
}