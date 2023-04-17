using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Modules.Warehouse.Shared.ValueObjects;

namespace Brewup.Modules.Warehouse.Shared.Helpers;

public static class BeerDepositoRowHelper
{
	public static BeerDepositRow ToValueObject(BeerDepositRowJson json) => new BeerDepositRow(new BeerId(json.BeerId),
		new BeerName(json.BeerName), new MovementQuantity(json.MovementQuantity));
}