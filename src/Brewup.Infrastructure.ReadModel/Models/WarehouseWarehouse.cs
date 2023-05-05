using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Infrastructure.ReadModel.Models;

public class WarehouseWarehouse : ModelBase
{
	public string WarehouseName { get; private set; } = string.Empty;

	protected WarehouseWarehouse()
	{ }

	public static WarehouseWarehouse Create(WarehouseId warehouseId, WarehouseName warehouseName) =>
		new(warehouseId.ToString(), warehouseName.Value);

	private WarehouseWarehouse(string warehouseId, string warehouseName)
	{
		Id = warehouseId;
		WarehouseName = warehouseName;
	}

	public WarehouseJson ToJson() => new()
	{
		WarehouseId = Id,
		WarehouseName = WarehouseName
	};
}