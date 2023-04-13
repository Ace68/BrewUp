namespace Brewup.Modules.Warehouse.Shared.Dtos;

public record DeliveryOrderRow(string RowId,
	string RowNumber,
	string BeerId,
	string BeerName,
	double Quantity,
	double UnitPrice);