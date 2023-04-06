using Brewup.Infrastructure.Muflone;
using Brewup.Infrastructure.ReadModel.MongoDb;
using Brewup.Shared.Configuration;

namespace Brewup.Modules;

public class InfrastructureModule : IModule
{
	public bool IsEnabled => true;
	public int Order => 0;

	public void RegisterModule(WebApplicationBuilder builder)
	{
		builder.Services.AddMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);
		builder.Services.AddEventstoreMongoDb(builder.Configuration.GetSection("BrewUp:MongoDbSettings").Get<MongoDbSettings>()!);
		builder.Services.AddMuflone(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>()!);
	}

	public void MapEndpoints(IEndpointRouteBuilder endpoints)
	{
		// do nothing
	}
}