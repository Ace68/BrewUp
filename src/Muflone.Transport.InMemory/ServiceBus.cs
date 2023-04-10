using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Abstracts;

namespace Muflone.Transport.InMemory;

public class ServiceBus : IServiceBus, IEventBus
{
	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<ServiceBus> _logger;
	private readonly ISerializer _messageSerializer;

	public ServiceBus(IServiceProvider serviceProvider,
		ILogger<ServiceBus> logger)
	{
		_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_messageSerializer = new Serializer();
	}

	public Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
	{
		if (command == null)
			throw new ArgumentNullException(nameof(command));

		return SendAsyncCore(command, cancellationToken);
	}

	private async Task SendAsyncCore<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand
	{
		MufloneBroker.Send(command);
	}

	public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
	{
		if (@event == null)
			throw new ArgumentNullException(nameof(@event));

		return PublishAsyncCore(@event, cancellationToken);
	}

	public async Task PublishAsyncCore<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
	{
		MufloneBroker.Publish(@event);
	}

	private async Task PublishDomainEventAsyncCore<T>(T @event, CancellationToken cancellationToken = default)
		where T : class, IDomainEvent
	{
		var domainEventConsumer = _serviceProvider.GetService<IDomainEventConsumer<T>>();
		if (domainEventConsumer == null)
			throw new ArgumentNullException($"No DomainEventConsumer was registered for {typeof(T).FullName}");

		await domainEventConsumer.ConsumeAsync(@event, cancellationToken);
	}

	private async Task PublishIntegrationEventAsyncCore<T>(T @event, CancellationToken cancellationToken = default)
		where T : class, IIntegrationEvent
	{
		var integrationEventConsumer = _serviceProvider.GetService<IIntegrationEventConsumer<T>>();
		if (integrationEventConsumer == null)
			throw new ArgumentNullException($"No IntegrationEventConsumer was registered for {typeof(T).FullName}");

		await integrationEventConsumer.ConsumeAsync(@event, cancellationToken);
	}
}