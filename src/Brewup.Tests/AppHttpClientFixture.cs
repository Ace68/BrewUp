using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net.Http;

namespace Brewup.Tests;

public class AppHttpClientFixture
{
	public readonly HttpClient Client;

	private const string Token =
		"Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIwMDljYzE4Yy03YzBiLTQ0NDMtYjlmMy01MWQzNDllNGY3ZTMiLCJ1c2VyTmFtZSI6ImFsYmVydG8uYWNlcmJpc0BnbWFpbC5jb20iLCJyb2xlIjoiVSIsImxpY2Vuc2VUeXBlIjoiQmFzZSIsInJlZnJlc2hUb2tlbiI6IlcyZnZxM0hnaSs0YjZRbkdtRHFUcmc9PSIsImFwcGxpY2F0aW9uS2V5IjoiWXEwV1VuT3pBUzdYR05IeE9rQnYiLCJyZW1lbWJlck1lIjoiVHJ1ZSIsIm5iZiI6MTY2ODgwNTE1NSwiZXhwIjoxNzAwMzQxMTU1LCJpc3MiOiJodHRwczovL2Fyb24tYXV0aGVudGljYXRpb24uYXp1cmV3ZWJzaXRlcy5uZXQvdG9rZW4iLCJhdWQiOiJ3aW5kaW5nIn0.hSPL1zTUO4Uy5SFiSPdJri6KsvOCCZuJAgBZIMA4sxDU0lDEwMS-V8TvzvZHp90zbkTYoANeRQ98z7lZWGcY4g";

	public AppHttpClientFixture()
	{
		var app = new ProjectsApplication();
		Client = app.CreateClient();
		//Client.DefaultRequestHeaders.Add("Authorization", Token);
	}

	private class ProjectsApplication : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			//var mongoDbSettings = new MongoDbSettings
			//{
			//	ConnectionString = "mongodb://localhost",

			//	DatabaseName = "Winding-Database"
			//};

			builder.ConfigureServices(services =>
			{
				services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
				//services.AddSharedModule();

				Log.Logger = new LoggerConfiguration()
					.WriteTo.File("Logs\\Brewup.log")
					.CreateLogger();

				//services.AddMongoDb(mongoDbSettings);
			});

			return base.CreateHost(builder);
		}
	}
}