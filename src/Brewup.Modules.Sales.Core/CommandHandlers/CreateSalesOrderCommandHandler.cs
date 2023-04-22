using Brewup.Modules.Sales.Core.Abstracts;
using Brewup.Modules.Sales.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Sales.Core.CommandHandlers;

public class CreateSalesOrderCommandHandler : CommandHandlerAsync<CreateSalesOrder>
{
	public CreateSalesOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override Task HandleAsync(CreateSalesOrder command, CancellationToken cancellationToken = new())
	{
		try
		{

		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}