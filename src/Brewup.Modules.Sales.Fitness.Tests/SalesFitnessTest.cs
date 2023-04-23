using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Brewup.Modules.Sales.Abstracts;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Brewup.Modules.Sales.Fitness.Tests;

public class SalesFitnessTest
{
	private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(ISalesOrchestrator).Assembly)
		.Build();

	private readonly IObjectProvider<IType> _forbiddenLayer = Types().
		That().
		ResideInNamespace("Brewup.Modules.Warehouse").
		As("Forbidden Layer");
	private readonly IObjectProvider<Interface> _forbiddenInterfaces = Interfaces().
		That().
		HaveFullNameContaining("Warehouse").
		As("Forbidden Interfaces");

	[Fact]
	public void SalesTypesShouldBeInCorrectLayer()
	{
		IArchRule forbiddenInterfacesShouldBeInForbiddenLayer =
			Interfaces().That().Are(_forbiddenInterfaces).Should().Be(_forbiddenLayer);

		forbiddenInterfacesShouldBeInForbiddenLayer.Check(Architecture);
	}
}