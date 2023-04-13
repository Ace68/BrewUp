namespace Brewup.Modules.Sales.Shared.Dtos;

public class SalesOrderRowJson
{
	public string RowId { get; set; } = string.Empty;
	public string RowNumber { get; set; } = string.Empty;
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;
	public double QuantityOrdered { get; set; } = 0;
	public double QuantityDelivered { get; set; } = 0;
	public double UnitPrice { get; set; } = 0;
}