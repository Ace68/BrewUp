using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Core;

namespace Brewup.Modules.Stores.Core.Entities;

public class SparesAvailability : AggregateRoot
{
	private readonly SpareId _spareId;
	private readonly Stock _stock;
	private readonly Availability _availability;
	private readonly ProductionCommitted _productionCommitted;
	private readonly SalesCommitted _salesCommitted;
	private readonly SupplierOrdered _supplierOrdered;

	protected SparesAvailability()
	{ }

	#region constructor
	internal static SparesAvailability CreateSparesAvailability(SpareId spareId)
	{
		return new SparesAvailability(spareId, new Stock(0), new Availability(0), new ProductionCommitted(0), new SalesCommitted(0), new SupplierOrdered(0));
	}

	private SparesAvailability(SpareId spareId, Stock stock, Availability availability, ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered)
	{
		_spareId = spareId;
		_stock = stock;
		_availability = availability;
		_productionCommitted = productionCommitted;
		_salesCommitted = salesCommitted;
		_supplierOrdered = supplierOrdered;
	}
	#endregion
}