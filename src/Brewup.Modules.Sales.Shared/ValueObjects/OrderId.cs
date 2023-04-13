using Muflone.Core;

namespace Brewup.Modules.Sales.Shared.ValueObjects;

public sealed class OrderId : DomainId
{
	public OrderId(Guid value) : base(value)
	{
	}
}