using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Stores.Infrastructure.Consumers.Commands;

public sealed class AskForBeerAvailabilityConsumer : CommandConsumerBase<AskForBeerAvailability>
{
	protected override ICommandHandlerAsync<AskForBeerAvailability> HandlerAsync { get; }

	public AskForBeerAvailabilityConsumer(IRepository repository, ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new AskForBeerAvailabilityCommandHandler(repository, loggerFactory);
	}
}