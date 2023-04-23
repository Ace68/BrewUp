namespace Brewup.Modules.Warehouse.Shared.Dtos;

public class BeerJson
{
	public string BeerId { get; set; } = string.Empty;
	public string BeerName { get; set; } = string.Empty;

	public double Stock { get; set; }
	public double Availability { get; set; }
	public double SalesCommitted { get; set; }
}