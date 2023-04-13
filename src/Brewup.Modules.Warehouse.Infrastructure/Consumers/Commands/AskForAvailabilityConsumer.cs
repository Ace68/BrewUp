using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;

public sealed class AskForAvailabilityConsumer : CommandConsumerBase<AskForAvailability>
{
	protected override ICommandHandlerAsync<AskForAvailability> HandlerAsync { get; }

	public AskForAvailabilityConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new AskForAvailabilityCommandHandler(repository, loggerFactory);
	}
}