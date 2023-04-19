using Brewup.Infrastructure.Muflone;
using Brewup.Infrastructure.ReadModel.MongoDb;
using Brewup.Modules.Sales.Sagas;
using Brewup.Shared.Configuration;
using Muflone.Eventstore;
using Muflone.Saga.Persistence.MongoDb;

namespace Brewup.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 99;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);
		builder.Services.AddEventstoreMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);
		builder.Services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions("mongodb://localhost", "BrewUp"));

		builder.Services.AddMufloneEventStore(builder.Configuration["BrewUp:EventStoreSettings:ConnectionString"]!);
		builder.Services.AddMuflone();

		builder.Services.AddScoped<PurchaseOrderSaga>();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// do nothing
	}
}