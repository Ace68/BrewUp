using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Purchases.Abstracts;

public class PurchaseLogger
{
	public readonly ILogger Logger;

	public PurchaseLogger(ILoggerFactory loggerFactory)
	{
		Logger = loggerFactory.CreateLogger(GetType());
	}
}