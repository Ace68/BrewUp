namespace Brewup.Modules.Sales.Shared.ValueObjects;

public record TotalAmount
{
	public double Value { get; init; }

	public TotalAmount(double value)
	{
		if (value < 0)
			throw new ArgumentOutOfRangeException(nameof(value), "Total amount cannot be negative");

		Value = value;
	}
}