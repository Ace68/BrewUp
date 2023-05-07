﻿using Brewup.Infrastructure.ReadModel.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Abstracts;

public abstract class SalesBaseService
{
	protected readonly IPersister Persister;
	protected readonly ILogger Logger;

	protected SalesBaseService(IServiceProvider serviceProvider,
		ILoggerFactory loggerFactory)
	{
		var persisters = serviceProvider.GetServices<IPersister>();
		Persister = persisters.FirstOrDefault(x => x.Type == "SalesPersiter");
		Logger = loggerFactory.CreateLogger(GetType());
	}
}