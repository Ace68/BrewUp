using Brewup.Modules.Sales.Core.Abstracts;
using Brewup.Modules.Sales.Core.Entities;
using Brewup.Modules.Sales.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Sales.Core.CommandHandlers;

public class CreateSalesOrderCommandHandler : CommandHandlerAsync<CreateSalesOrder>
{
	public CreateSalesOrderCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(CreateSalesOrder command, CancellationToken cancellationToken = new())
	{
		try
		{
			var aggregate = SalesOrder.CreateSalesOrder(command.OrderId, command.OrderNumber, command.OrderDate,
				command.CustomerId, command.CustomerName, command.TotalAmount, command.Rows, command.MessageId);
			await Repository.SaveAsync(aggregate, Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}