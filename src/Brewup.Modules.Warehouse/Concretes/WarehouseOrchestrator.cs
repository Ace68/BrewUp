using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;

using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Concretes;

internal sealed class WarehouseOrchestrator : IWarehouseOrchestrator
{
	private readonly IWarehouseService _warehouseService;
	private readonly IBeerService _beerService;
	private readonly ILogger _logger;

	public WarehouseOrchestrator(
		IWarehouseService warehouseService,
		IBeerService beerService,
		ILoggerFactory loggerFactory)
	{
		_warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
		_beerService = beerService ?? throw new ArgumentNullException(nameof(beerService));

		_logger = loggerFactory.CreateLogger(GetType());
	}

	public async Task<string> CreateWarehouse(WarehouseJson warehouseToCreate, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(warehouseToCreate.WarehouseId))
			warehouseToCreate.WarehouseId = Guid.NewGuid().ToString();

		await _warehouseService.CreateWarehouseAsync(new WarehouseId(new Guid(warehouseToCreate.WarehouseId)),
			new WarehouseName(warehouseToCreate.WarehouseName), cancellationToken);

		return warehouseToCreate.WarehouseId;
	}

	public async Task AddBeerDepositAsync(BeerDepositJson beerDeposit, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(beerDeposit.MovementId))
			beerDeposit.MovementId = Guid.NewGuid().ToString();

		foreach (var rowJson in beerDeposit.Rows)
		{
			await _beerService.CreateBeerAsync(new BeerId(rowJson.BeerId), new BeerName(rowJson.BeerName),
				cancellationToken);
			await _beerService.UpdateStoreQuantityAsync(new BeerId(rowJson.BeerId),
				new Stock(rowJson.MovementQuantity), new Availability(rowJson.MovementQuantity), cancellationToken);
		}
	}

	public async Task<IEnumerable<BeerWithdrawn>> WithdrawalBeerFromWarehouseAsync(WarehouseId warehouseId, IEnumerable<BeerToDrawn> beersToDrawn,
		CancellationToken cancellationToken)
	{
		var beerWithdrawn = new List<BeerWithdrawn>();

		foreach (var beerToDrawn in beersToDrawn)
		{
			var beer = await _beerService.GetBeerAsync(beerToDrawn.BeerId, cancellationToken);
			if (string.IsNullOrWhiteSpace(beer.BeerId))
			{
				beerWithdrawn.Add(new BeerWithdrawn(beerToDrawn.BeerId, beerToDrawn.Quantity, new Stock(0),
					new Availability(0)));
			}
			else
			{
				if (beer.Availability >= beerToDrawn.Quantity.Value)
				{
					beerWithdrawn.Add(new BeerWithdrawn(beerToDrawn.BeerId, beerToDrawn.Quantity, new Stock(beer.Stock),
						new Availability(beer.Availability)));
					await _beerService.UpdateStoreQuantityAsync(beerToDrawn.BeerId, new Stock(beer.Stock - beerToDrawn.Quantity.Value),
												new Availability(beer.Availability - beerToDrawn.Quantity.Value), cancellationToken);
				}

				if (beer.Availability < beerToDrawn.Quantity.Value)
				{
					beerWithdrawn.Add(new BeerWithdrawn(beerToDrawn.BeerId, beerToDrawn.Quantity, new Stock(0),
						new Availability(0)));
					await _beerService.UpdateSalesCommittesAsync(beerToDrawn.BeerId,
						new SalesCommitted(beerToDrawn.Quantity.Value), cancellationToken);
				}
			}
		}

		return beerWithdrawn;
	}
}