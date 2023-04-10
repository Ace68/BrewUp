using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.InMemory.Abstracts;

namespace Muflone.Transport.InMemory.Consumers;

public abstract class IntegrationEventConsumerBase<T> : IIntegrationEventConsumer<T>, IAsyncDisposable where T : class, IIntegrationEvent
{
	public string TopicName { get; }

	private readonly Persistence.ISerializer _messageSerializer;
	private readonly ILogger _logger;

	protected abstract IEnumerable<IIntegrationEventHandlerAsync<T>> HandlersAsync { get; }

	protected IntegrationEventConsumerBase(ILoggerFactory loggerFactory,
		Persistence.ISerializer? messageSerializer = null)
	{
		TopicName = typeof(T).Name;

		_logger = loggerFactory.CreateLogger(GetType()) ?? throw new ArgumentNullException(nameof(loggerFactory));
		_messageSerializer = messageSerializer ?? new Persistence.Serializer();
	}

	public Task StartAsync(CancellationToken cancellationToken = default)
	{
		MufloneBroker.IntegrationEvents.CollectionChanged += OnEventReceived;

		return Task.CompletedTask;
	}

	public Task StopAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

	public async Task ConsumeAsync(T message, CancellationToken cancellationToken = default)
	{
		try
		{
			if (message == null)
				throw new ArgumentNullException(nameof(message));

			foreach (var handlerAsync in HandlersAsync)
			{
				await handlerAsync.HandleAsync((dynamic)message, cancellationToken);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex,
				$"An error occurred processing domainEvent {typeof(T).Name}. StackTrace: {ex.StackTrace} - Source: {ex.Source} - Message: {ex.Message}");
			throw;
		}
	}

	private void OnEventReceived(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs @event)
	{
		if (@event.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
		{
			foreach (var item in @event.NewItems)
			{
				if (item is T message)
				{
					Task.Run(async () => await ConsumeAsync(message));
					MufloneBroker.IntegrationEvents.Remove(message);
				}
			}
		}
	}

	#region Dispose
	public ValueTask DisposeAsync() => ValueTask.CompletedTask;
	#endregion
}