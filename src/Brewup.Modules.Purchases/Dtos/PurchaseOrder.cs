namespace Brewup.Modules.Purchases.Dtos;

public class PurchaseOrder
{
	public string OrderId { get; set; } = string.Empty;
	public string OrderNumber { get; set; } = string.Empty;
	public DateTime OrderDate { get; set; } = DateTime.Now;
	public string CustomerId { get; set; } = string.Empty;
	public string CustomerName { get; set; } = string.Empty;
	public double TotalAmount { get; set; } = 0;
	public IEnumerable<PurchaseOrderRow> Rows { get; set; } = Enumerable.Empty<PurchaseOrderRow>();
}