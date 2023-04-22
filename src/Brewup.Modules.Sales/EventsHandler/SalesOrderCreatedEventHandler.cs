using Brewup.Modules.Sales.Abstracts;
using Brewup.Modules.Sales.Shared.DomainEvents;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace Brewup.Modules.Sales.EventsHandler;

public sealed class SalesOrderCreatedEventHandler : DomainEventHandlerAsync<SalesOrderCreated>
{
	private readonly ISalesOrderService _salesOrderService;

	public SalesOrderCreatedEventHandler(ILoggerFactory loggerFactory,
		ISalesOrderService salesOrderService) : base(loggerFactory)
	{
		_salesOrderService = salesOrderService;
	}

	public override async Task HandleAsync(SalesOrderCreated @event, CancellationToken cancellationToken = new())
	{
		try
		{
			await _salesOrderService.AddOrderAsync(@event, cancellationToken);
		}
		catch (Exception ex)
		{
			throw;
		}
	}
}