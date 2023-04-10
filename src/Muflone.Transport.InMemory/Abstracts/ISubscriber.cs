using Muflone.Messages;

namespace Muflone.Transport.InMemory.Abstracts;

public interface ISubscriber
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}

public interface ISubscriber<T> : ISubscriber where T : IMessage
{
}