using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class BeerDepositValidator : AbstractValidator<BeerDepositJson>
{
	public BeerDepositValidator()
	{
		RuleFor(v => v.WarehouseId).NotEmpty();
		RuleFor(v => v.MovementDate).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
		RuleFor(v => v.CausalId).NotEmpty();
		RuleFor(v => v.CausalDescription).NotEmpty();

		RuleForEach(v => v.Rows).SetValidator(new BeerDepositRowValidator());
	}
}