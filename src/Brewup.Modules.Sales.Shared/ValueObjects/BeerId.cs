using Muflone.Core;

namespace Brewup.Modules.Sales.Shared.ValueObjects;

public sealed class BeerId : DomainId
{
	public BeerId(Guid value) : base(value)
	{
	}
}