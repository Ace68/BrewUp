using Brewup.Modules.Stores.Abstracts;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Modules.Stores.Shared.Dtos;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Muflone.Persistence;

namespace Brewup.Modules.Stores.Concretes;

public sealed class StoresOrchestrator : IStoresOrchestrator
{
	private readonly IServiceBus _serviceBus;

	public StoresOrchestrator(IServiceBus serviceBus)
	{
		_serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));
	}

	public async Task CreateAvailabilityAsync(SpareAvailability body, CancellationToken cancellationToken)
	{
		var command = new CreateSpareAvailability(new SpareId(new Guid(body.SpareId)),
			new Stock(body.Stock),
			new Availability(body.Availability),
			new ProductionCommitted(body.ProductionCommitted),
			new SalesCommitted(body.SalesCommitted),
			new SupplierOrdered(body.SupplierOrdered));

		await _serviceBus.SendAsync(command, cancellationToken);
	}
}