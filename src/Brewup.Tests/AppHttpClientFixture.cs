using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net.Http;

namespace Brewup.Tests;

public class AppHttpClientFixture
{
	public readonly HttpClient Client;

	public AppHttpClientFixture()
	{
		var app = new ProjectsApplication();
		Client = app.CreateClient();
	}

	private class ProjectsApplication : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

				Log.Logger = new LoggerConfiguration()
					.WriteTo.File("Logs\\Brewup.log")
					.CreateLogger();
			});

			return base.CreateHost(builder);
		}
	}
}