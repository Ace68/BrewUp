using Xunit;

namespace Brewup.Tests;

[CollectionDefinition("Integration Fixture")]
public abstract class IntegrationCollectionFixture : ICollectionFixture<AppHttpClientFixture>
{
}