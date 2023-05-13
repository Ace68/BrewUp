using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Brewup.Modules.Warehouse.Abstracts;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Brewup.Modules.Warehouse.Fitness.Tests
{
	public class WarehouseFitnessTest
	{
		private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(IWarehouseOrchestrator).Assembly).Build();

		private readonly IObjectProvider<IType> _forbiddenModule = Types().
			That().
			ResideInNamespace("Brewup.Modules.Sales").
			As("Forbidden Layer");
		private readonly IObjectProvider<Interface> _forbiddenInterfaces = Interfaces().
			That().
			HaveFullNameContaining("Sales").
			As("Forbidden Interfaces");

		[Fact]
		public void WarehouseTypesShouldBeInCorrectModule()
		{
			IArchRule forbiddenInterfacesShouldBeInForbiddenModule =
				Interfaces().That().Are(_forbiddenInterfaces).Should().Be(_forbiddenModule);

			forbiddenInterfacesShouldBeInForbiddenModule.Check(Architecture);
		}
	}
}