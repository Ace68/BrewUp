using Brewup.Modules.Production.Abstracts;
using Microsoft.Extensions.Logging;

namespace Brewup.Modules.Production.Concretes;

public sealed class ProductionService : ProductionBaseService, IProductionService
{
    public ProductionService(ILoggerFactory loggerFactory) : base(loggerFactory)
    {
    }
}