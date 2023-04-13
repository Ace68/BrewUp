using Brewup.Modules.Warehouse.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Warehouse.Validators;

public class SpareAvailabilityValidator : AbstractValidator<SpareAvailabilityJson>
{
	public SpareAvailabilityValidator()
	{
		RuleFor(v => v.SpareId).NotEmpty();
	}
}