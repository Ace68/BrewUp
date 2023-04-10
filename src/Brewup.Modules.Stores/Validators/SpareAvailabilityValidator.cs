using Brewup.Modules.Stores.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Stores.Validators;

public class SpareAvailabilityValidator : AbstractValidator<SpareAvailabilityJson>
{
	public SpareAvailabilityValidator()
	{
		RuleFor(v => v.SpareId).NotEmpty();
	}
}