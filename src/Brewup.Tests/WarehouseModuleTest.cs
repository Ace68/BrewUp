using Brewup.Modules.Warehouse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Brewup.Tests;

[Collection("Integration Fixture")]
public class WarehouseModuleTest
{
	private readonly AppHttpClientFixture _integrationFixture;

	public WarehouseModuleTest(AppHttpClientFixture integrationFixture)
	{
		_integrationFixture = integrationFixture;
	}

	[Fact]
	public async Task CreateWarehouse_Should_Throw_With_Invalid_Json()
	{
		var body = new WarehouseJson
		{
			WarehouseId = string.Empty,
			WarehouseName = string.Empty
		};

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/warehouses", httpContent);

		Assert.Equal(HttpStatusCode.BadRequest, postResult.StatusCode);
	}

	[Fact]
	public async Task CreateWarehouse_Should_Not_Throw_With_Valid_Json()
	{
		var body = new WarehouseJson
		{
			WarehouseId = string.Empty,
			WarehouseName = "Muflone Storage"
		};

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/warehouses", httpContent);

		Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);
	}

	[Fact]
	public async Task AddBeerDeposit_Should_Throw_With_Invalid_Json()
	{
		var body = new BeerDepositJson
		{
			WarehouseId = Guid.NewGuid().ToString(),
			MovementDate = DateTime.UtcNow,

			CausalId = string.Empty,
			CausalDescription = "Versamento Prodotto Finito a Magazzino",

			Rows = new List<BeerDepositRowJson>
			{
				new(Guid.NewGuid().ToString(), "Muflone IPA", 10)
			}
		};

		var stringJson = JsonSerializer.Serialize(body);
		var httpContent = new StringContent(stringJson, Encoding.UTF8, "application/json");
		var postResult = await _integrationFixture.Client.PostAsync("/v1/warehouses/deposit", httpContent);

		Assert.Equal(HttpStatusCode.BadRequest, postResult.StatusCode);
	}
}