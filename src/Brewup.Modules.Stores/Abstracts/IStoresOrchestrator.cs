using Brewup.Modules.Stores.Shared.Dtos;

namespace Brewup.Modules.Stores.Abstracts;

public interface IStoresOrchestrator
{
	Task CreateAvailabilityAsync(SpareAvailabilityJson body, CancellationToken cancellationToken);
}