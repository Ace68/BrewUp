using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;

public sealed class CreateSpareAvailabilityConsumer : CommandConsumerBase<CreateSpareAvailability>
{
	protected override ICommandHandlerAsync<CreateSpareAvailability> HandlerAsync { get; }

	public CreateSpareAvailabilityConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new CreateSpareAvailabilityCommandHandler(repository, loggerFactory);
	}
}