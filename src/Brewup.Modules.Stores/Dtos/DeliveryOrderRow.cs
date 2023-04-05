namespace Brewup.Modules.Stores.Dtos;

public record DeliveryOrderRow(string RowId,
	string RowNumber,
	string BeerId,
	string BeerName,
	double Quantity,
	double UnitPrice);