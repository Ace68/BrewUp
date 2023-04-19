namespace Brewup.Modules.Sales.Shared.Dtos;

public class SalesOrderJson
{
	public string OrderId { get; set; } = string.Empty;
	public string OrderNumber { get; set; } = string.Empty;
	public DateTime OrderDate { get; set; } = DateTime.Now;

	public string WarehouseId { get; set; } = string.Empty;

	public string CustomerId { get; set; } = string.Empty;
	public string CustomerName { get; set; } = string.Empty;

	public double TotalAmount { get; set; } = 0;
	public IEnumerable<SalesOrderRowJson> Rows { get; set; } = Enumerable.Empty<SalesOrderRowJson>();
}