using Brewup.Modules.Warehouse.Core.Abstracts;
using Brewup.Modules.Warehouse.Core.Entities;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Warehouse.Core.CommandHandlers;

public sealed class AskForBeerAvailabilityCommandHandler : CommandHandlerAsync<AskForBeerAvailability>
{
	public AskForBeerAvailabilityCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(AskForBeerAvailability command, CancellationToken cancellationToken = new())
	{
		try
		{
			var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
			beer.CheckAvailability();

			await Repository.SaveAsync(beer, Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}