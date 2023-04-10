using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;

namespace Brewup.Infrastructure.ReadModel.Models;

public class Beer : ModelBase
{
	public string BeerName { get; private set; }

	protected Beer()
	{ }

	public static Beer CreateBeer(BeerId beerId, BeerName beerName) => new(beerId.ToString(), beerName.Value);

	private Beer(string beerId, string beerName)
	{
		Id = beerId;
		BeerName = beerName;
	}

	public BeerJson ToJson() => new()
	{
		BeerId = Id,
		BeerName = BeerName
	};
}