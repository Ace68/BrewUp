﻿using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Concretes;

public sealed class WarehouseService : WarehouseBaseService, IWarehouseService
{
	public WarehouseService(ILoggerFactory loggerFactory,
		IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task CreateWarehouseAsync(WarehouseId warehouseId, WarehouseName warehouseName,
		CancellationToken cancellationToken)
	{
		try
		{
			var warehouse = Infrastructure.ReadModel.Models.Warehouse.Create(warehouseId, warehouseName);

			await Persister.InsertAsync(warehouse, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}