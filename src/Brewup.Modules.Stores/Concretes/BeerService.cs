using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.Concretes;

public sealed class BeerService : StoreBaseService, IBeerService
{
	public BeerService(ILoggerFactory loggerFactory,
		IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task<string> CreateBeerAsync(BeerJson beerToCreate, CancellationToken cancellationToken)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(beerToCreate.BeerId))
				beerToCreate.BeerId = Guid.NewGuid().ToString();

			var beer = Beer.CreateBeer(new BeerId(new Guid(beerToCreate.BeerId)), new BeerName(beerToCreate.BeerName));
			await Persister.InsertAsync(beer, cancellationToken);

			return beer.Id;
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}