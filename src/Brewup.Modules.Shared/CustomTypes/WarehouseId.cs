using Muflone.Core;

namespace Brewup.Modules.Shared.CustomTypes;

public sealed class WarehouseId : DomainId
{
	public WarehouseId(Guid value) : base(value)
	{
	}
}