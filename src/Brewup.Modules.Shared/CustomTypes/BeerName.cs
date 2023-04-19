namespace Brewup.Modules.Shared.CustomTypes;

public record BeerName
{
	public string Value { get; init; }

	public BeerName(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new ArgumentException("Beer name cannot be empty.", nameof(value));

		Value = value;
	}
}