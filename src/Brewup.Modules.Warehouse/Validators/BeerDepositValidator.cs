using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class BeerDepositValidator : AbstractValidator<BeerDepositJson>
{
	public BeerDepositValidator()
	{
		RuleFor(v => v.BeerId).NotEmpty();
		RuleFor(v => v.StoreId).NotEmpty();
		RuleFor(v => v.MovementDate).GreaterThan(DateTime.MinValue).LessThan(DateTime.MaxValue);
		RuleFor(v => v.MovementQuantity).GreaterThan(0);
		RuleFor(v => v.CausalId).NotEmpty();
		RuleFor(v => v.CausalDescription).NotEmpty();
	}
}