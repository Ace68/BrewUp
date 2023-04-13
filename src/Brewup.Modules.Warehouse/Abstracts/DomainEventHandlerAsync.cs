using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace Brewup.Modules.Warehouse.Abstracts;

public abstract class DomainEventHandlerAsync<TEvent> : IDomainEventHandlerAsync<TEvent> where TEvent : class, IDomainEvent
{
	protected ILogger Logger;

	protected DomainEventHandlerAsync(ILoggerFactory loggerFactory)
	{
		Logger = loggerFactory.CreateLogger(GetType());
	}

	public abstract Task HandleAsync(TEvent @event, CancellationToken cancellationToken = new());

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~DomainEventHandlerAsync()
	{
		Dispose(false);
	}
	#endregion
}