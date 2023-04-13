using Brewup.Modules.Purchases.Shared.Dtos;

namespace Brewup.Modules.Purchases.Sagas;

public class PurchaseSagaState
{
	public Guid SagaId { get; set; } = Guid.NewGuid();

	public PurchaseOrderJson Order { get; set; } = new();
	public bool StoreAvailabilityChecked { get; set; } = false;
	public bool PaymentAvailabilityChecked { get; set; } = false;
	public bool DeliveryOrderLaunched { get; set; } = false;
}