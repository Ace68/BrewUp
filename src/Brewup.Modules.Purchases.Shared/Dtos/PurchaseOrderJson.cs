namespace Brewup.Modules.Purchases.Shared.Dtos;

public class PurchaseOrderJson
{
	public string OrderId { get; set; } = string.Empty;
	public string OrderNumber { get; set; } = string.Empty;
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public string CustomerId { get; set; } = string.Empty;
	public string CustomerName { get; set; } = string.Empty;
	public double TotalAmount { get; set; } = 0;
	public IEnumerable<PurchaseOrderRowJson> Rows { get; set; } = Enumerable.Empty<PurchaseOrderRowJson>();
}