using Brewup.Modules.Sales.Infrastructure.Consumers.Commands;
using Brewup.Modules.Sales.Infrastructure.Consumers.Events;
using Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;
using Brewup.Modules.Warehouse.Infrastructure.Consumers.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Muflone;
using Muflone.Persistence;
using Muflone.Transport.InMemory;
using Muflone.Transport.InMemory.Abstracts;

namespace Brewup.Infrastructure.Muflone;

public static class MufloneHelper
{
	public static IServiceCollection AddMuflone(this IServiceCollection services)
	{
		services.AddSingleton<IServiceBus, ServiceBus>();
		services.AddSingleton<IEventBus, ServiceBus>();

		var serviceProvider = services.BuildServiceProvider();
		var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
		var repository = serviceProvider.GetService<IRepository>();

		var consumers = new List<IConsumer>
		{
			#region Warehouse
			new CreateWarehouseConsumer(repository!, loggerFactory!),
			new WarehouseCreatedConsumer(serviceProvider, loggerFactory!),

			new AddBeerDepositConsumer(repository!, loggerFactory!),
			new BeerDepositAddedConsumer(serviceProvider, loggerFactory!),

			new AskForBeersAvailabilityConsumer(repository!, loggerFactory!),

			new WithdrawalFromWarehouseConsumer(repository!, loggerFactory!),
			#endregion

			#region Sales
			//new BroadcastWarehouseCreatedConsumer(serviceProvider, loggerFactory!),
			new BeersAvailabilityAskedConsumer(serviceProvider, loggerFactory!),
			new BroadcastBeerWithdrawnConsumer(serviceProvider, loggerFactory!),
			new BeerWithdrawnConsumer(serviceProvider, loggerFactory!),

			new CreateSalesOrderConsumer(repository!, loggerFactory!),
			new SalesOrderCreatedConsumer(serviceProvider, loggerFactory!),
			#endregion
		};

		services.AddMufloneTransportInMemory(consumers);

		return services;
	}
}