using Brewup.Infrastructure.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Sales.Abstracts;

public abstract class PurchaseBaseService
{
	protected readonly IPersister Persister;
	protected readonly ILogger Logger;

	protected PurchaseBaseService(IPersister persister,
		ILoggerFactory loggerFactory)
	{
		Persister = persister ?? throw new ArgumentNullException(nameof(persister));
		Logger = loggerFactory.CreateLogger(GetType());
	}
}