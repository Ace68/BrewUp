using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Stores.Shared.Commands;

public sealed class CreateBeerAvailability : Command
{
	public readonly BeerId BeerId;
	public readonly Stock Stock;
	public readonly Availability Availability;

	public CreateBeerAvailability(BeerId aggregateId, Stock stock, Availability availability) : base(aggregateId)
	{
		BeerId = aggregateId;

		Stock = stock;
		Availability = availability;
	}
}