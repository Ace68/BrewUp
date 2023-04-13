using Brewup.Modules.Sales.Shared.Dtos;

namespace Brewup.Modules.Sales.Sagas;

public class SalesSagaState
{
	public Guid SagaId { get; set; } = Guid.NewGuid();

	public SalesOrderJson Order { get; set; } = new();
	public bool StoreAvailabilityChecked { get; set; } = false;
	public bool PaymentAvailabilityChecked { get; set; } = false;
	public bool DeliveryOrderLaunched { get; set; } = false;
}