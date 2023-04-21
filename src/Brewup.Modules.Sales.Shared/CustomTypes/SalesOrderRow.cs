using Brewup.Modules.Shared.CustomTypes;

namespace Brewup.Modules.Sales.Shared.CustomTypes;

public record SalesOrderRow(RowId RowId,
	BeerId BeerId,
	BeerName BeerName,
	QuantityOrdered QuantityOrdered,
	QuantityDelivered QuantityDelivered,
	UnitPrice UnitPrice);