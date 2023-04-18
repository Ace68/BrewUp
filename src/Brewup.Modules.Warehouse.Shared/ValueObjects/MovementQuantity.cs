﻿namespace Brewup.Modules.Warehouse.Shared.ValueObjects;

public record MovementQuantity
{
	public double Value { get; init; }

	public MovementQuantity(double value)
	{
		if (value < 0)
			throw new ArgumentOutOfRangeException(nameof(value), "Movement quantity cannot be negative");

		Value = value;
	}
}