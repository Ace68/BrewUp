using Brewup.Modules.Stores.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Stores.Validators;

public class BeerValidator : AbstractValidator<BeerJson>
{
	public BeerValidator()
	{
		RuleFor(v => v.BeerName).NotEmpty();
	}
}