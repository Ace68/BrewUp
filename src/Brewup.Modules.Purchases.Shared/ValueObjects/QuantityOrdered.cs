﻿namespace Brewup.Modules.Purchases.Shared.ValueObjects;

public record QuantityOrdered
{
	public double Value { get; init; }

	public QuantityOrdered(double value)
	{
		if (value < 0)
		{
			throw new ArgumentException("Quantity ordered cannot be negative.", nameof(value));
		}
		Value = value;
	}
}