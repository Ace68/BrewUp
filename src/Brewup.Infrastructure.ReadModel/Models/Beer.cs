using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Modules.Warehouse.Shared.ValueObjects;

namespace Brewup.Infrastructure.ReadModel.Models;

public class Beer : ModelBase
{
	public string BeerName { get; private set; }

	public double Stock { get; private set; }
	public double Availability { get; private set; }
	public double SalesCommitted { get; private set; }

	protected Beer()
	{ }

	public static Beer CreateBeer(BeerId beerId, BeerName beerName) => new(beerId.Value, beerName.Value);

	private Beer(string beerId, string beerName)
	{
		Id = beerId;
		BeerName = beerName;

		Stock = 0;
		Availability = 0;
		SalesCommitted = 0;
	}

	public void UpdateStoreQuantity(Stock stock, Availability availability)
	{
		Stock = stock.Value;
		Availability = availability.Value;
	}

	public BeerJson ToJson() => new()
	{
		BeerId = Id,
		BeerName = BeerName
	};
}