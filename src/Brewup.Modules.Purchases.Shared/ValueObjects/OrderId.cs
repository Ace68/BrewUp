using Muflone.Core;

namespace Brewup.Modules.Purchases.Shared.ValueObjects;

public sealed class OrderId : DomainId
{
	public OrderId(Guid value) : base(value)
	{
	}
}