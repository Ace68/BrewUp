namespace Brewup.Modules.Warehouse.Shared.Dtos;

public class BeerDepositJson
{
	public string WarehouseId { get; set; } = string.Empty;

	public string MovementId { get; set; } = string.Empty;
	public DateTime MovementDate { get; set; } = DateTime.MinValue;

	public string CausalId { get; set; } = string.Empty;
	public string CausalDescription { get; set; } = string.Empty;

	public IEnumerable<BeerDepositRowJson> Rows { get; set; } = new List<BeerDepositRowJson>();

}