using Brewup.Infrastructure.ReadModel.Models;
using Brewup.Modules.Shared.CustomTypes;
using Brewup.Modules.Warehouse.Abstracts;
using Brewup.Modules.Warehouse.Shared.Dtos;
using Brewup.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Concretes;

internal sealed class SparesAvailabilityService : WarehouseBaseService, ISparesAvailabilityService
{
	public SparesAvailabilityService(ILoggerFactory loggerFactory,
		IServiceProvider serviceProvider) : base(loggerFactory, serviceProvider)
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