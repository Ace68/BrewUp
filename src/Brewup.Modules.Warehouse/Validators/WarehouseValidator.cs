using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class WarehouseValidator : AbstractValidator<WarehouseJson>
{
	public WarehouseValidator()
	{
		RuleFor(v => v.WarehouseName).NotEmpty();
	}
}