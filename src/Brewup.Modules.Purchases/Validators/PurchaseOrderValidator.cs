﻿using Brewup.Modules.Purchases.Shared.Dtos;
using FluentValidation;

namespace Brewup.Modules.Purchases.Validators;

public class PurchaseOrderValidator : AbstractValidator<PurchaseOrderJson>
{
	public PurchaseOrderValidator()
	{
		RuleFor(v => v.OrderDate).GreaterThan(DateTime.MaxValue).WithMessage("OrderDate is mandatory!");
		RuleFor(v => v.CustomerId).NotNull().NotEmpty().WithMessage("CustomerId is mandatory!");
		RuleFor(v => v.CustomerName).NotNull().NotEmpty().WithMessage("CustomerName is mandatory!");

		RuleForEach(v => v.Rows).SetValidator(new PurchaseOrderRowValidator());
	}
}