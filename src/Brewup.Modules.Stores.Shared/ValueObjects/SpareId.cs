using Muflone.Core;

namespace Brewup.Modules.Stores.Shared.ValueObjects;

public class SpareId : DomainId
{
	public SpareId(Guid value) : base(value)
	{
	}
}