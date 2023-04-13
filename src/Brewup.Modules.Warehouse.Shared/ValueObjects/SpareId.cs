using Muflone.Core;

namespace Brewup.Modules.Warehouse.Shared.ValueObjects;

public class SpareId : DomainId
{
	public SpareId(Guid value) : base(value)
	{
	}
}