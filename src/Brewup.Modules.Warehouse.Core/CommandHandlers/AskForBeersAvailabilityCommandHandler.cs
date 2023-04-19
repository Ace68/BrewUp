using Brewup.Modules.Shared.Commands;
using Brewup.Modules.Warehouse.Core.Abstracts;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Warehouse.Core.CommandHandlers;

public sealed class AskForBeersAvailabilityCommandHandler : CommandHandlerAsync<AskForBeersAvailability>
{
	public AskForBeersAvailabilityCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(AskForBeersAvailability command, CancellationToken cancellationToken = new())
	{
		try
		{
			var warehouse = await Repository.GetByIdAsync<Entities.Warehouse>(command.WarehouseId.Value);
			warehouse.AskForBeersAvailability(command.Beers, command.MessageId);
			await Repository.SaveAsync(warehouse, Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}