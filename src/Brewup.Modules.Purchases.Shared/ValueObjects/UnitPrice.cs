namespace Brewup.Modules.Purchases.Shared.ValueObjects;

public record UnitPrice
{
	public double Value { get; init; }

	public UnitPrice(double value)
	{
		if (value < 0)
			throw new ArgumentException("Unit price cannot be negative.", nameof(value));

		Value = value;
	}
}