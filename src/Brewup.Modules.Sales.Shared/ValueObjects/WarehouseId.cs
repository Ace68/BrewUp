using Muflone.Core;

namespace Brewup.Modules.Sales.Shared.ValueObjects;

public sealed class WarehouseId : DomainId
{
	public WarehouseId(Guid value) : base(value)
	{
	}
}