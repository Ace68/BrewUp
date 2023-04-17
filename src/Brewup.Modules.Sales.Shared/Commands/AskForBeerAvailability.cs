using Brewup.Modules.Sales.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Sales.Shared.Commands;

public sealed class AskForBeerAvailability : Command
{
	public readonly BeerId BeerId;

	public AskForBeerAvailability(BeerId aggregateId, Guid commitId) : base(aggregateId, commitId)
	{
		BeerId = aggregateId;
	}
}