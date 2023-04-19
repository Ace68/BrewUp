using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Shared.Dtos;

namespace Brewup.Modules.Warehouse.Shared.Helpers;

public static class BeerDepositoRowHelper
{
	public static BeerDepositRow ToValueObject(BeerDepositRowJson json) => new BeerDepositRow(new BeerId(json.BeerId),
		new BeerName(json.BeerName), new MovementQuantity(json.MovementQuantity));
}