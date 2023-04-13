using Brewup.Modules.Warehouse.Core.Abstracts;
using Brewup.Modules.Warehouse.Core.Entities;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Warehouse.Core.CommandHandlers;

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