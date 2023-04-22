using Brewup.Modules.Sales.Core.CommandHandlers;
using Brewup.Modules.Sales.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Sales.Infrastructure.Consumers.Commands;

public sealed class CreateSalesOrderConsumer : CommandConsumerBase<CreateSalesOrder>
{
	protected override ICommandHandlerAsync<CreateSalesOrder> HandlerAsync { get; }

	public CreateSalesOrderConsumer(IRepository repository, ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new CreateSalesOrderCommandHandler(repository, loggerFactory);
	}
}