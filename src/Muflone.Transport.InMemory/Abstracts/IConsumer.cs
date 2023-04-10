using Muflone.Messages.Commands;
using Muflone.Messages.Events;

namespace Muflone.Transport.InMemory.Abstracts;

public interface IConsumer
{
    string TopicName { get; }
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}

public interface IDomainEventConsumer<in T> : IConsumer where T : class, IDomainEvent
{
    Task ConsumeAsync(T message, CancellationToken cancellationToken = default);
}

public interface IIntegrationEventConsumer<in T> : IConsumer where T : class, IIntegrationEvent
{
    Task ConsumeAsync(T message, CancellationToken cancellationToken = default);
}

public interface ICommandConsumer<in T> : IConsumer where T : class, ICommand
{
    Task ConsumeAsync(T message, CancellationToken cancellationToken = default);
}