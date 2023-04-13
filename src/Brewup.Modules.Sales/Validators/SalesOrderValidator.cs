using Brewup.Modules.Sales.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Sales.Validators;

public class SalesOrderValidator : AbstractValidator<SalesOrderJson>
{
	public SalesOrderValidator()
	{
		RuleFor(v => v.OrderDate).GreaterThan(DateTime.MinValue).WithMessage("OrderDate is mandatory!");
		RuleFor(v => v.CustomerId).NotNull().NotEmpty().WithMessage("CustomerId is mandatory!");
		RuleFor(v => v.CustomerName).NotNull().NotEmpty().WithMessage("CustomerName is mandatory!");

		RuleForEach(v => v.Rows).SetValidator(new PurchaseOrderRowValidator());
	}
}