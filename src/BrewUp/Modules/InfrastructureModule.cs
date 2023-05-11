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
		var mongoSettings = builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!;

		builder.Services.AddMongoDb(mongoSettings);
		builder.Services.AddEventstoreMongoDb(mongoSettings);
		builder.Services.AddMongoSagaStateRepository(new MongoSagaStateRepositoryOptions(mongoSettings.ConnectionString, mongoSettings.DatabaseName));
		builder.Services.AddMufloneEventStore(builder.Configuration["BrewUp:EventStoreSettings:ConnectionString"]!);
		builder.Services.AddMuflone();

		builder.Services.AddScoped<SalesOrderSaga>();
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// do nothing
	}
}