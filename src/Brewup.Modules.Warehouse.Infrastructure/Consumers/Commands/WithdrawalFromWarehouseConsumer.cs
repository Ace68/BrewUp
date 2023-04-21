using Brewup.Modules.Shared.Commands;
using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;

public sealed class WithdrawalFromWarehouseConsumer : CommandConsumerBase<WithdrawalFromWarehouse>
{
	protected override ICommandHandlerAsync<WithdrawalFromWarehouse> HandlerAsync { get; }

	public WithdrawalFromWarehouseConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new WithdrawalFromWarehouseCommandHandler(repository, loggerFactory);
	}
}