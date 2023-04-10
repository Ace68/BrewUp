using Brewup.Infrastructure.ReadModel.Abstracts;
using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.Concretes;

public sealed class SparesAvailabilityService : StoreBaseService, ISparesAvailabilityService
{
	public SparesAvailabilityService(ILoggerFactory loggerFactory,
		IPersister persister) : base(loggerFactory, persister)
	{
	}

	public async Task CreateAvailabilityAsync(SpareId spareId, Stock stock, Availability availability,
		ProductionCommitted productionCommitted, SalesCommitted salesCommitted, SupplierOrdered supplierOrdered,
		CancellationToken cancellationToken)
	{
		try
		{
			var sparesAvailability = SparesAvailability.CreateSparesAvailability(spareId, stock, availability,
				productionCommitted, salesCommitted, supplierOrdered);

			await Persister.InsertAsync(sparesAvailability, cancellationToken);
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}

	public async Task<SpareAvailabilityJson> GetAvailabilityAsync(SpareId spareId, CancellationToken cancellationToken)
	{
		try
		{
			var sparesAvailability = await Persister.GetByIdAsync<SparesAvailability>(spareId.ToString(), cancellationToken);

			return sparesAvailability.ToJson();
		}
		catch (Exception ex)
		{
			Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
			throw;
		}
	}
}