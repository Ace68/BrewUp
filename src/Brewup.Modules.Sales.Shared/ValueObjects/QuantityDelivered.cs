namespace Brewup.Modules.Sales.Shared.ValueObjects;

public record QuantityDelivered
{
	public double Value { get; init; }

	public QuantityDelivered(double value)
	{
		if (value < 0)
		{
			throw new ArgumentException("Quantity delivered cannot be negative.", nameof(value));
		}
		Value = value;
	}
}