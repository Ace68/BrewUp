using Muflone.Core;

namespace Brewup.Modules.Stores.Shared.ValueObjects;

public sealed class BeerId : DomainId
{
	public BeerId(Guid value) : base(value)
	{
	}
}