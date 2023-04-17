using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Warehouse.Shared.ValueObjects;

namespace Brewup.Infrastructure.ReadModel.Models;

public class Warehouse : ModelBase
{
	public string WarehouseName { get; private set; } = string.Empty;

	protected Warehouse()
	{ }

	public static Warehouse Create(WarehouseId warehouseId, WarehouseName warehouseName) =>
		new(warehouseId.ToString(), warehouseName.Value);

	private Warehouse(string warehouseId, string warehouseName)
	{
		Id = warehouseId;
		WarehouseName = warehouseName;
	}
}