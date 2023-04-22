using Brewup.Modules.Sales.Core.Entities;
using Brewup.Modules.Sales.Shared.CustomTypes;

namespace Brewup.Modules.Sales.Core.Helpers;

public static class SalesCoreHelpers
{
	public static OrderRow ToEntity(this SalesOrderRow row) => new(row.RowId, row.BeerId, row.BeerName,
		row.QuantityOrdered, row.QuantityDelivered, row.UnitPrice);
}