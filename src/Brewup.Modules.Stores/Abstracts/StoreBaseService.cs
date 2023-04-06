using Brewup.Infrastructure.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Stores.Abstracts;

public abstract class StoreBaseService
{
	protected readonly IPersister Persister;
	protected readonly ILogger Logger;

	protected StoreBaseService(ILoggerFactory loggerFactory, IPersister persister)
	{
		Persister = persister ?? throw new ArgumentNullException(nameof(persister));
		Logger = loggerFactory.CreateLogger(GetType());
	}
}