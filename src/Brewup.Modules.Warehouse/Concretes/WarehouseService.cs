using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Concretes;

internal sealed class WarehouseService : WarehouseBaseService, IWarehouseService
{
	public WarehouseService(ILoggerFactory loggerFactory,
		IServiceProvider serviceProvider) : base(loggerFactory, serviceProvider)
	{
	}

	public async Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName,
		CancellationToken cancellationToken)
	{
		try
		{
			var warehouse = WarehouseWarehouse.Create(warehouseId, warehouseName);

			await Persister.InsertAsync(warehouse, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task<IEnumerable<WarehouseJson>> GetWarehousesAsync(CancellationToken cancellationToken)
	{
		try
		{
			var warehouses = await Persister.FindAsync<WarehouseWarehouse>();
			var warehousesArray = warehouses as WarehouseWarehouse[] ?? warehouses.ToArray();

			return warehousesArray.Any()
				? warehousesArray.Select(w => w.ToJson())
				: Enumerable.Empty<WarehouseJson>();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}