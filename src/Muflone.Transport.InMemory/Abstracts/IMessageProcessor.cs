namespace Muflone.Transport.InMemory.Abstracts;

public interface IMessageProcessor
{
    Task ProcessAsync<T>(T message, CancellationToken cancellationToken = default);
}