namespace Brewup.Modules.Shared.CustomTypes;

public record BeerWithdrawn(BeerId BeerId, Quantity Quantity, Stock Stock, Availability Availability);