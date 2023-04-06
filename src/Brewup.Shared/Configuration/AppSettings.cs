namespace Brewup.Shared.Configuration;

public class AppSettings
{

}

public class TokenAuthentication
{
	public string SecretKey { get; set; } = string.Empty;
	public string Issuer { get; set; } = string.Empty;
	public string Audience { get; set; } = string.Empty;
}

public class EventStoreSettings
{
	public string ConnectionString { get; set; } = string.Empty;
}

public class MongoDbSettings
{
	public string ConnectionString { get; set; } = string.Empty;
	public string DatabaseName { get; set; } = string.Empty;
}