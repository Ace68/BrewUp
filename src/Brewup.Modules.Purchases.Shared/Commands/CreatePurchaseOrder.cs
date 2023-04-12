using Brewup.Modules.Purchases.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Purchases.Shared.Commands;

public sealed class CreatePurchaseOrder : Command
{
	public readonly BeerId BeerId;

	public CreatePurchaseOrder(BeerId aggregateId, Guid commitId) : base(aggregateId, commitId)
	{
		BeerId = aggregateId;
	}
}