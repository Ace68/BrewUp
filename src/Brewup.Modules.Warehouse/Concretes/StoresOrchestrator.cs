using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Commands;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Muflone.Core;
using Muflone.Persistence;

namespace Brewup.Modules.Warehouse.Concretes;

public sealed class StoresOrchestrator : IStoresOrchestrator
{
	private readonly IBeerService _beerService;
	private readonly IServiceBus _serviceBus;
	private readonly ILogger _logger;

	public StoresOrchestrator(
		IBeerService beerService,
		IServiceBus serviceBus,
		ILoggerFactory loggerFactory)
	{
		_beerService = beerService ?? throw new ArgumentNullException(nameof(beerService));
		_serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(beerToCreate.BeerId))
			beerToCreate.BeerId = Guid.NewGuid().ToString();

		var createBeer = new CreateBeer(new BeerId(new Guid(beerToCreate.BeerId)),
						new BeerName(beerToCreate.BeerName));
		await _serviceBus.SendAsync(createBeer, cancellationToken);

		return beerToCreate.BeerId;
	}

	public async Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken)
	{
		var beer = await _beerService.GetBeerAsync(new BeerId(new Guid(beerDeposit.BeerId)), cancellationToken);
		if (string.IsNullOrWhiteSpace(beer.BeerId))
			throw new AggregateNotFoundException(new Guid(beerDeposit.BeerId), typeof(BeerId));

		var addBeerDeposit = new AddBeerDeposit(new BeerId(new Guid(beerDeposit.BeerId)),
						new StoreId(new Guid(beerDeposit.StoreId)),
									new MovementId(Guid.NewGuid().ToString()),
									new MovementDate(beerDeposit.MovementDate),
									new CausalId(beerDeposit.CausalId),
									new CausalDescription(beerDeposit.CausalDescription),
									new MovementQuantity(beerDeposit.MovementQuantity));

		await _serviceBus.SendAsync(addBeerDeposit, cancellationToken);
	}

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