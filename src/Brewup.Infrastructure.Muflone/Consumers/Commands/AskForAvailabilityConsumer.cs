using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Infrastructure.Muflone.Consumers.Commands;

public sealed class AskForAvailabilityConsumer : CommandConsumerBase<AskForAvailability>
{
	protected override ICommandHandlerAsync<AskForAvailability> HandlerAsync { get; }

	public AskForAvailabilityConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new AskForAvailabilityCommandHandler(repository, loggerFactory);
	}
}