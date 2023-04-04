using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Production.Abstracts;

public abstract class ProductionBaseService
{
    protected readonly ILogger Logger;

    protected ProductionBaseService(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}