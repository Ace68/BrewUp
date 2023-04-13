﻿using Brewup.Modules.Sales.Infrastructure.Consumers.Commands;
using Brewup.Modules.Sales.Infrastructure.Consumers.Events;
using Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;
using Brewup.Modules.Warehouse.Infrastructure.Consumers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;
using Muflone.Transport.InMemory;
using Muflone.Transport.InMemory.Abstracts;

namespace Brewup.Infrastructure.Muflone;

public static class MufloneHelper
{
	public static IServiceCollection AddMuflone(this IServiceCollection services)
	{
		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		var repository = serviceProvider.GetService<IRepository>();

		var consumers = new List<IConsumer>
		{
			#region Warehouse
			new CreateBeerConsumer(repository!, loggerFactory!),
			new BeerCreatedConsumer(serviceProvider, loggerFactory!),

			new AddBeerDepositConsumer(repository!, loggerFactory!),
			new BeerDepositAddedConsumer(serviceProvider, loggerFactory!),

			new AskForBeerAvailabilityConsumer(repository!, loggerFactory!),
			#endregion

			#region Sales
			new BeerAvailabilityCheckedConsumer(serviceProvider, loggerFactory!),
			new LaunchSalesOrderSagaConsumer(loggerFactory!),
			#endregion
		};

		services.AddMufloneTransportInMemory(consumers);

		return services;
	}
}