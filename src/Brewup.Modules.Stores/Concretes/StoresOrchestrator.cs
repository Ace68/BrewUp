using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Dtos;

namespace Brewup.Modules.Stores.Concretes;

public sealed class StoresOrchestrator : IStoresOrchestrator
{


	public Task CreateAvailabilityAsync(SpareAvailability body, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}