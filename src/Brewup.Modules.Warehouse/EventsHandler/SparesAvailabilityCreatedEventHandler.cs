using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.DomainEvents;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.EventsHandler;

public sealed class SparesAvailabilityCreatedEventHandler : DomainEventHandlerAsync<SparesAvailabilityCreated>
{
	private readonly ISparesAvailabilityService _sparesAvailabilityService;

	public SparesAvailabilityCreatedEventHandler(ILoggerFactory loggerFactory,
		ISparesAvailabilityService sparesAvailabilityService) : base(loggerFactory)
	{
		_sparesAvailabilityService = sparesAvailabilityService;
	}

	public override async Task HandleAsync(SparesAvailabilityCreated @event, CancellationToken cancellationToken = new())
	{
		try
		{
			await _sparesAvailabilityService.CreateAvailabilityAsync(@event.SpareId,
				@event.Stock,
				@event.Availability,
				@event.ProductionCommitted,
				@event.SalesCommitted,
				@event.SupplierOrdered,
				cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}