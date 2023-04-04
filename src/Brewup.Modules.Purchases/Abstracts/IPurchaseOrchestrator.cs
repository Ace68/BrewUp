﻿using Brewup.Modules.Purchases.Dtos;

namespace Brewup.Modules.Purchases.Abstracts;

public interface IPurchaseOrchestrator
{
	Task<string> AddOrderAsync(Order orderToAdd, CancellationToken cancellationToken);
}