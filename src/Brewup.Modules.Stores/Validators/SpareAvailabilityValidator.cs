using Brewup.Modules.Stores.Dtos;
using FluentValidation;

namespace Brewup.Modules.Stores.Validators;

public class SpareAvailabilityValidator : AbstractValidator<SpareAvailability>
{
	public SpareAvailabilityValidator()
	{
		RuleFor(v => v.SpareId).NotEmpty();
	}
}