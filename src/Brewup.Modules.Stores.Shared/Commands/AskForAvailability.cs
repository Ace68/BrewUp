using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Stores.Shared.Commands;

public sealed class AskForAvailability : Command
{
	public readonly SpareId SpareId;

	public AskForAvailability(SpareId aggregateId) : base(aggregateId)
	{
		SpareId = aggregateId;
	}
}