using Brewup.Modules.Shared.CustomTypes;
using Muflone.Core;

namespace Brewup.Modules.Warehouse.Core.Entities;

public class Beer : Entity
{
	public BeerId BeerId;
	private BeerName _beerName;

	private Stock _stock;
	private Availability _availability;

	private ProductionCommitted _productionCommitted;
	private SalesCommitted _salesCommitted;
	private SupplierOrdered _supplierOrdered;

	protected Beer() { }

	public Stock GetStock() => _stock;
	public Availability GetAvailability() => _availability;
	public ProductionCommitted GetProductionCommitted() => _productionCommitted;
	public SalesCommitted GetSalesCommitted() => _salesCommitted;
	public SupplierOrdered GetSupplierOrdered() => _supplierOrdered;

	#region constructor
	internal static Beer CreateBeer(BeerId beerId, BeerName beerName)
	{
		return new Beer(beerId, beerName);
	}

	private Beer(BeerId beerId, BeerName beerName)
	{
		BeerId = beerId;
		_beerName = beerName;

		_stock = new Stock(0);
		_availability = new Availability(0);

		_productionCommitted = new ProductionCommitted(0);
		_salesCommitted = new SalesCommitted(0);
		_supplierOrdered = new SupplierOrdered(0);
	}
	#endregion

	internal void UpdateAvailabilities(Stock stock)
	{
		_stock = stock;
		_availability = new Availability(_stock.Value - _productionCommitted.Value - _salesCommitted.Value +
										 _supplierOrdered.Value);
	}
}