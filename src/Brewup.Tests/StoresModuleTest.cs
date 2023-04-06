using Brewup.Modules.Stores.Shared.Dtos;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Brewup.Tests;

[Collection("Integration Fixture")]
public class StoresModuleTest
{
	private readonly AppHttpClientFixture _integrationFixture;

	public StoresModuleTest(AppHttpClientFixture integrationFixture)
	{
		_integrationFixture = integrationFixture;
	}

	[Fact]
	public async Task Cannot_Send_InvalidJson()
	{
		var body = new SpareAvailability(string.Empty,
			0,
			0,
			0,
			0,
			0);

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/stores/availability", httpContent);

		Assert.Equal(HttpStatusCode.BadRequest, postResult.StatusCode);
	}

	[Fact]
	public async Task Should_Send_ValidJson()
	{
		var body = new SpareAvailability(Guid.NewGuid().ToString(),
			0,
			0,
			0,
			0,
			0);

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/stores/availability", httpContent);

		Assert.Equal(HttpStatusCode.Accepted, postResult.StatusCode);
	}
}