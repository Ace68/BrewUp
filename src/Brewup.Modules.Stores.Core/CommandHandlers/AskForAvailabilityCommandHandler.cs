using Brewup.Modules.Stores.Core.Abstracts;
using Brewup.Modules.Stores.Core.Entities;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Core.CommandHandlers;

public sealed class AskForAvailabilityCommandHandler : CommandHandlerAsync<AskForAvailability>
{
	public AskForAvailabilityCommandHandler(IRepository repository,
		ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(AskForAvailability command, CancellationToken cancellationToken = new())
	{
		var sparesAvailability =
			await Repository.GetByIdAsync<SparesAvailability>(command.SpareId.Value);

		sparesAvailability.CheckAvailability();

		await Repository.SaveAsync(sparesAvailability, Guid.NewGuid());
	}
}