﻿using Brewup.Modules.Stores.Dtos;

namespace Brewup.Modules.Stores.Abstracts;

public interface IStoresOrchestrator
{
	Task CreateAvailabilityAsync(SpareAvailability body, CancellationToken cancellationToken);
}