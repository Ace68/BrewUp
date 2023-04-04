using Brewup.Modules.Purchases.Dtos;
using FluentValidation;

namespace Brewup.Modules.Purchases.Validators;

public class OrderRowValidator : AbstractValidator<OrderRow>
{
	public OrderRowValidator()
	{
		RuleFor(v => v.BeerId).NotNull().NotEmpty().WithMessage("BeerId is mandatory!");
		RuleFor(v => v.BeerName).NotNull().NotEmpty().WithMessage("BeerName is mandatory!");
		RuleFor(v => v.QuantityOrdered).GreaterThan(0).WithMessage("QuantityOrdered is mandatory!");
		RuleFor(v => v.UnitPrice).GreaterThan(0).WithMessage("UnitPrice is mandatory!");
	}
}