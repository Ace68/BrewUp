using Muflone.Core;

namespace Brewup.Modules.Warehouse.Shared.ValueObjects;

public sealed class StoreId : DomainId
{
	public StoreId(Guid value) : base(value)
	{
	}
}