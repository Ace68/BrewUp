using Brewup.Modules.Shared.CustomTypes;
using Muflone.Core;

namespace Brewup.Modules.Sales.Core.Entities;

public class OrderRow : Entity
{
	private RowId _rowId;

	private BeerId _beerId;
	private BeerName _beerName;

	private QuantityOrdered _quantityOrdered;
	private QuantityDelivered _quantityDelivered;
	private UnitPrice _unitPrice;

	protected OrderRow()
	{ }

	internal OrderRow(RowId rowId, BeerId beerId, BeerName beerName, QuantityOrdered quantityOrdered,
		QuantityDelivered quantityDelivered, UnitPrice unitPrice)
	{
		_rowId = rowId;
		_beerId = beerId;
		_beerName = beerName;
		_quantityOrdered = quantityOrdered;
		_quantityDelivered = quantityDelivered;
		_unitPrice = unitPrice;
	}
}