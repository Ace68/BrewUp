﻿using Brewup.Modules.Warehouse.Shared.ValueObjects;
using Muflone.Messages.Commands;

namespace Brewup.Modules.Warehouse.Shared.Commands;

public sealed class AskForBeerAvailability : Command
{
	public readonly WarehouseId WarehouseId;

	public AskForBeerAvailability(WarehouseId aggregateId) : base(aggregateId)
	{
		WarehouseId = aggregateId;
	}
}