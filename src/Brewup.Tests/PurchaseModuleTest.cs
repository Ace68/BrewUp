using Brewup.Modules.Purchases.Dtos;
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
public class PurchaseModuleTest
{
	private readonly AppHttpClientFixture _integrationFixture;

	public PurchaseModuleTest(AppHttpClientFixture integrationFixture)
	{
		_integrationFixture = integrationFixture;
	}

	[Fact]
	public async Task Cannot_Send_InvalidJson()
	{
		var body = new PurchaseOrder
		{
			OrderId = string.Empty,
			OrderNumber = string.Empty,
			OrderDate = DateTime.MinValue,
			CustomerId = string.Empty,
			CustomerName = string.Empty,
			TotalAmount = 0,
			Rows = Enumerable.Empty<PurchaseOrderRow>()
		};

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/purchases", httpContent);

		Assert.Equal(HttpStatusCode.BadRequest, postResult.StatusCode);
	}
}