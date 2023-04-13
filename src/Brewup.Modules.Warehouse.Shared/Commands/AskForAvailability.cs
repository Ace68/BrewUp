using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class AskForAvailability : Command
{
	public readonly SpareId SpareId;

	public AskForAvailability(SpareId aggregateId) : base(aggregateId)
	{
		SpareId = aggregateId;
	}
}