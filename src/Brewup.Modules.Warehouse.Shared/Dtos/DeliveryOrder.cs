namespace Brewup.Modules.Warehouse.Shared.Dtos;

public record DeliveryOrder(string OrderId,
	string OrderNumber,
	DateTime OrderDate,
	string CustomerId,
	string CustomerName,
	string Address,
	double TotalAmount,
	IEnumerable<DeliveryOrderRow> Rows);