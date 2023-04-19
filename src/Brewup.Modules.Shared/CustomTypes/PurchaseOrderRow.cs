namespace Brewup.Modules.Shared.CustomTypes;

public record PurchaseOrderRow(OrderId OrderId, RowId RowId, RowNumber RowNumber,
	BeerId BeerId, BeerName BeerName,
	QuantityOrdered QuantityOrdered, QuantityDelivered QuantityDelivered,
	UnitPrice UnitPrice, TotalAmount TotalAmount);