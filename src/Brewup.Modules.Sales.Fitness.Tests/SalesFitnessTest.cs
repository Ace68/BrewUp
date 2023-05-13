using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Brewup.Modules.Sales.Abstracts;

using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Brewup.Modules.Sales.Fitness.Tests
{
	public class SalesFitnessTest
	{
		private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(ISalesOrchestrator).Assembly).Build();

		private readonly IObjectProvider<IType> _salesModule =
			Types().That().ResideInAssembly("Brewup.Modules.Sales").As("Sales Module");

		private readonly IObjectProvider<Class> _salesClasses =
			Classes().That().ImplementInterface("ISalesOrderService").As("Sales Classes");

		private readonly IObjectProvider<IType> _forbiddenModule = Types().
			That().
			ResideInNamespace("Brewup.Modules.Warehouse").
			As("Forbidden Layer");
		private readonly IObjectProvider<Interface> _forbiddenInterfaces = Interfaces().
			That().
			HaveFullNameContaining("Warehouse").
			As("Forbidden Interfaces");

		[Fact]
		public void SalesTypesShouldBeInCorrectModule()
		{
			IArchRule forbiddenInterfacesShouldBeInForbiddenLayer =
				Interfaces().That().Are(_forbiddenInterfaces).Should().Be(_forbiddenModule);

			forbiddenInterfacesShouldBeInForbiddenLayer.Check(Architecture);
		}

		[Fact]
		public void TypesShouldBeInCorrectModule()
		{
			IArchRule exampleClassesShouldBeInSalesModule =
				Classes().That().Are(_salesClasses).Should().Be(_salesModule);
			IArchRule forbiddenInterfacesShouldBeInForbiddenLayer =
				Interfaces().That().Are(_forbiddenInterfaces).Should().Be(_forbiddenModule);

			//check if your architecture fulfills your rules
			exampleClassesShouldBeInSalesModule.Check(Architecture);
			forbiddenInterfacesShouldBeInForbiddenLayer.Check(Architecture);

			//you can also combine your rules
			IArchRule combinedArchRule =
				exampleClassesShouldBeInSalesModule
					.And(forbiddenInterfacesShouldBeInForbiddenLayer);

			combinedArchRule.Check(Architecture);
		}
	}
}