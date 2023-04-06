using Brewup.Modules.Stores.Core.Abstracts;
using Brewup.Modules.Stores.Core.Entities;
using Brewup.Modules.Stores.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Core.CommandHandlers;

public sealed class CreateSpareAvailabilityCommandHandler : CommandHandlerAsync<CreateSpareAvailability>
{
	public CreateSpareAvailabilityCommandHandler(IRepository repository,
		ILoggerFactory loggerFactory) : base(repository, loggerFactory)
	{
	}

	public override async Task HandleAsync(CreateSpareAvailability command, CancellationToken cancellationToken = new())
	{
		var spareAvailability = SparesAvailability.CreateSparesAvailability(command.SpareId,
			command.Stock,
			command.Availability,
			command.ProductionCommitted,
			command.SalesCommitted,
			command.SupplierOrdered);

		await Repository.SaveAsync(spareAvailability, Guid.NewGuid());
	}
}