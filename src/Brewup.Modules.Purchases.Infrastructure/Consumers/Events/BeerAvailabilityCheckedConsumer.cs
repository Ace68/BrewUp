using Brewup.Modules.Purchases.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Purchases.Infrastructure.Consumers.Events;

public sealed class BeerAvailabilityCheckedConsumer : DomainEventConsumerBase<BeerAvailabilityChecked>
{
	protected override IEnumerable<IDomainEventHandlerAsync<BeerAvailabilityChecked>> HandlersAsync { get; }

	public BeerAvailabilityCheckedConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<BeerAvailabilityChecked>>();
	}
}