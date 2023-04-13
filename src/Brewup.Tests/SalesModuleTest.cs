using Brewup.Modules.Sales.Shared.Dtos;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Brewup.Tests;

[Collection("Integration Fixture")]
public class SalesModuleTest
{
	private readonly AppHttpClientFixture _integrationFixture;

	public SalesModuleTest(AppHttpClientFixture integrationFixture)
	{
		_integrationFixture = integrationFixture;
	}

	[Fact]
	public async Task Cannot_Send_InvalidJson()
	{
		var body = new SalesOrderJson
		{
			OrderId = string.Empty,
			OrderNumber = string.Empty,
			OrderDate = DateTime.MinValue,
			CustomerId = string.Empty,
			CustomerName = string.Empty,
			TotalAmount = 0,
			Rows = Enumerable.Empty<SalesOrderRowJson>()
		};

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/sales", httpContent);

		Assert.Equal(HttpStatusCode.BadRequest, postResult.StatusCode);
	}
}