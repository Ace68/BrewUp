using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Stores.Shared.Commands;

public sealed class AskForBeerAvailability : Command
{
	public readonly BeerId BeerId;

	public AskForBeerAvailability(BeerId aggregateId) : base(aggregateId)
	{
		BeerId = aggregateId;
	}
}