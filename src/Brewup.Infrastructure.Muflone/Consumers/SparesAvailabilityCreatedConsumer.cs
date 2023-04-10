using Brewup.Modules.Stores.Shared.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Infrastructure.Muflone.Consumers;

public sealed class SparesAvailabilityCreatedConsumer : DomainEventConsumerBase<SparesAvailabilityCreated>
{
	protected override IEnumerable<IDomainEventHandlerAsync<SparesAvailabilityCreated>> HandlersAsync { get; }

	public SparesAvailabilityCreatedConsumer(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory, ISerializer? messageSerializer = null) : base(loggerFactory, messageSerializer)
	{
		HandlersAsync = serviceProvider.GetServices<IDomainEventHandlerAsync<SparesAvailabilityCreated>>();
	}
}