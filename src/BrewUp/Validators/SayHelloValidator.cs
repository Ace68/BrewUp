using Brewup.Models;
using FluentValidation;

namespace Brewup.Validators;

public class SayHelloValidator : AbstractValidator<HelloRequest>
{
	public SayHelloValidator()
	{
		RuleFor(h => h.Name).NotEmpty().MaximumLength(50);
	}
}