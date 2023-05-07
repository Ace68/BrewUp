using Brewup.Infrastructure.ReadModel.Abstracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Abstracts;

public abstract class WarehouseBaseService
{
	protected readonly IPersister Persister;
	protected readonly ILogger Logger;

	protected WarehouseBaseService(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
	{
		var persisters = serviceProvider.GetServices<IPersister>();
		Persister = persisters.FirstOrDefault(x => x.Type == "Persister");
		Logger = loggerFactory.CreateLogger(GetType());
	}
}