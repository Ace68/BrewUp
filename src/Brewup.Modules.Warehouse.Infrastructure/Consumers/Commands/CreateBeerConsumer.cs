using Brewup.Modules.Warehouse.Core.CommandHandlers;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.InMemory.Consumers;

namespace Brewup.Modules.Warehouse.Infrastructure.Consumers.Commands;

public class CreateBeerConsumer : CommandConsumerBase<CreateBeer>
{
	protected override ICommandHandlerAsync<CreateBeer> HandlerAsync { get; }

	public CreateBeerConsumer(IRepository repository,
		ILoggerFactory loggerFactory) : base(loggerFactory)
	{
		HandlerAsync = new CreateBeerCommandHandler(repository, loggerFactory);
	}
}