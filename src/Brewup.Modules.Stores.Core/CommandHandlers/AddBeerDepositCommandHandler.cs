using Brewup.Modules.Stores.Core.Abstracts;
using Brewup.Modules.Stores.Core.Entities;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Core.CommandHandlers;

public sealed class AddBeerDepositCommandHandler : CommandHandlerAsync<AddBeerDeposit>
{
	public AddBeerDepositCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(AddBeerDeposit command, CancellationToken cancellationToken = new())
	{
		try
		{
			var beer = await Repository.GetByIdAsync<Beer>(command.BeerId.Value);
			beer.AddStoreDeposit(command.StoreId, command.MovementId, command.MovementDate, command.CausalId,
				command.CausalDescription, command.MovementQuantity);

			await Repository.SaveAsync(beer, Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}