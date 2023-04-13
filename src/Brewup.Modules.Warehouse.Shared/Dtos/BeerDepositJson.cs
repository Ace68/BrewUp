namespace Brewup.Modules.Warehouse.Shared.Dtos;

public record BeerDepositJson(string BeerId, string StoreId, DateTime MovementDate, string CausalId, string CausalDescription, double MovementQuantity);