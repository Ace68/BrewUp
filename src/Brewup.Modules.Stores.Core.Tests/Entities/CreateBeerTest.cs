using Brewup.Modules.Stores.Core.CommandHandlers;
using Brewup.Modules.Stores.Shared.Commands;
using Brewup.Modules.Stores.Shared.DomainEvents;
using Brewup.Modules.Stores.Shared.ValueObjects;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Messages.Commands;
using Muflone.Messages.Events;
using Muflone.SpecificationTests;

namespace Brewup.Modules.Stores.Core.Tests.Entities;

public sealed class CreateBeerTest : CommandSpecification<CreateBeer>
{
	private readonly BeerId _beerId = new(Guid.NewGuid());
	private readonly BeerName _beerName = new("Muflone IPA");

	protected override IEnumerable<DomainEvent> Given()
	{
		yield break;
	}

	protected override CreateBeer When() => new(_beerId, _beerName);

	protected override ICommandHandlerAsync<CreateBeer> OnHandler()
	{
		return new CreateBeerCommandHandler(Repository, new NullLoggerFactory());
	}

	protected override IEnumerable<DomainEvent> Expect()
	{
		yield return new BeerCreated(_beerId, _beerName);
	}
}