using Brewup.Infrastructure.ReadModel.MongoDb;
using Brewup.Modules.Sales.Sagas;
using Brewup.Shared.Configuration;

namespace Brewup.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 99;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);

		builder.Services.AddScoped<PurchaseOrderSaga>();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// do nothing
	}
}