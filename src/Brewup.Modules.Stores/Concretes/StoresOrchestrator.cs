using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Concretes;

public sealed class StoresOrchestrator : IStoresOrchestrator
{
	private readonly ISparesAvailabilityService _sparesAvailabilityService;
	private readonly IBeerService _beerService;
	private readonly IServiceBus _serviceBus;
	private readonly ILogger _logger;

	public StoresOrchestrator(ISparesAvailabilityService sparesAvailabilityService,
		IBeerService beerService,
		IServiceBus serviceBus,
		ILoggerFactory loggerFactory)
	{
		_sparesAvailabilityService = sparesAvailabilityService ?? throw new ArgumentNullException(nameof(sparesAvailabilityService));
		_beerService = beerService ?? throw new ArgumentNullException(nameof(beerService));
		_serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

		_logger = loggerFactory.CreateLogger(GetType());
	}

	public Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken) =>
		_beerService.CreateBeerAsync(beerToCreate, cancellationToken);

	public async Task CreateAvailabilityAsync(SpareAvailabilityJson body, CancellationToken cancellationToken)
	{
		try
		{
			var createSpareAvailability = new CreateSpareAvailability(new SpareId(new Guid(body.SpareId)),
				new Stock(body.Stock),
				new Availability(body.Availability),
				new ProductionCommitted(body.ProductionCommitted),
				new SalesCommitted(body.SalesCommitted),
				new SupplierOrdered(body.SupplierOrdered));

			await _serviceBus.SendAsync(createSpareAvailability, cancellationToken);
		}
		catch (Exception ex)
		{
			_logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}