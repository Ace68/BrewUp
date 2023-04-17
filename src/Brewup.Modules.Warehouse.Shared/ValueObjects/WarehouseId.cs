using Muflone.Core;

namespace Brewup.Modules.Warehouse.Shared.ValueObjects;

public sealed class WarehouseId : DomainId
{
	public WarehouseId(Guid value) : base(value)
	{
	}
}