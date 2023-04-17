using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class BeerDepositRowValidator : AbstractValidator<BeerDepositRowJson>
{
	public BeerDepositRowValidator()
	{
		RuleFor(v => v.BeerId).NotEmpty();
		RuleFor(v => v.BeerName).NotEmpty();
		RuleFor(v => v.MovementQuantity).GreaterThan(0).WithMessage("Quantity is Mandatory and must be Greater than 0!");
	}
}