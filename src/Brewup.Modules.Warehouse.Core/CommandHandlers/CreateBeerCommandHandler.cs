using Brewup.Modules.Warehouse.Core.Abstracts;
using Brewup.Modules.Warehouse.Core.Entities;
using Brewup.Modules.Warehouse.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Warehouse.Core.CommandHandlers;

public sealed class CreateBeerCommandHandler : CommandHandlerAsync<CreateBeer>
{
	public CreateBeerCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(CreateBeer command, CancellationToken cancellationToken = new())
	{
		try
		{
			var beer = Beer.CreateBeer(command.BeerId, command.Name);
			await Repository.SaveAsync(beer, Guid.NewGuid());
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}
}