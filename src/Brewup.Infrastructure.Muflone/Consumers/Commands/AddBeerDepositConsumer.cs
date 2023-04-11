using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Infrastructure.Muflone.Consumers.Commands;

public sealed class AddBeerDepositConsumer : CommandConsumerBase<AddBeerDeposit>
{
	protected override ICommandHandlerAsync<AddBeerDeposit> HandlerAsync { get; }

	public AddBeerDepositConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new AddBeerDepositCommandHandler(repository, loggerFactory);
	}
}