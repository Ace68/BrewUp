using Muflone.Core;

namespace Brewup.Modules.Shared.CustomTypes;

public sealed class OrderId : DomainId
{
	public OrderId(Guid value) : base(value)
	{
	}
}