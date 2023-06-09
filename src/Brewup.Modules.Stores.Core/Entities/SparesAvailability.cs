﻿using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Core;

namespace Brewup.Modules.Stores.Core.Entities;

public class SparesAvailability : AggregateRoot
{
	private SpareId _spareId;
	private Stock _stock;
	private Availability _availability;
	private ProductionCommitted _productionCommitted;
	private SalesCommitted _salesCommitted;
	private SupplierOrdered _supplierOrdered;

	protected SparesAvailability()
	{
	}

	#region constructor
	internal static SparesAvailability CreateSparesAvailability(SpareId spareId,
		Stock stock,
		Availability availability,
		ProductionCommitted productionCommitted,
		SalesCommitted salesCommitted,
		SupplierOrdered supplierOrdered)
	{
		return new SparesAvailability(spareId,
			stock,
			availability, productionCommitted,
			salesCommitted,
			supplierOrdered);
	}

	private SparesAvailability(SpareId spareId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered)
	{
		RaiseEvent(new SpareAvailabilityCreated(spareId, stock, availability, productionCommitted, salesCommitted,
			supplierOrdered));
	}

	private void Apply(SpareAvailabilityCreated @event)
	{
		_spareId = @event.SpareId;
		_stock = @event.Stock;
		_availability = @event.Availability;
		_productionCommitted = @event.ProductionCommitted;
		_salesCommitted = @event.SalesCommitted;
		_supplierOrdered = @event.SupplierOrdered;
	}
	#endregion
}