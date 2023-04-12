using Brewup.Modules.Stores.Core.Abstracts;
using Brewup.Modules.Stores.Core.Entities;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Core.CommandHandlers;

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