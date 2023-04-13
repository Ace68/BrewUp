using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class BeerValidator : AbstractValidator<BeerJson>
{
	public BeerValidator()
	{
		RuleFor(v => v.BeerName).NotEmpty();
	}
}