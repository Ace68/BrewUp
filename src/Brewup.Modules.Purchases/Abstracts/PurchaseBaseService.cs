using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Purchases.Abstracts;

public sealed class PurchaseBaseService
{
	public readonly ILogger Logger;

	public PurchaseBaseService(ILoggerFactory loggerFactory)
	{
		Logger = loggerFactory.CreateLogger(GetType());
	}
}