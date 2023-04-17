using Brewup.Infrastructure.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Warehouse.Abstracts;

public abstract class WarehouseBaseService
{
	protected readonly IPersister Persister;
	protected readonly ILogger Logger;

	protected WarehouseBaseService(ILoggerFactory loggerFactory, IPersister persister)
	{
		Persister = persister ?? throw new ArgumentNullException(nameof(persister));
		Logger = loggerFactory.CreateLogger(GetType());
	}
}