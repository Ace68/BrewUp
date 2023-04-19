using Muflone.Core;

namespace Brewup.Modules.Shared.CustomTypes;

public class SpareId : DomainId
{
	public SpareId(Guid value) : base(value)
	{
	}
}