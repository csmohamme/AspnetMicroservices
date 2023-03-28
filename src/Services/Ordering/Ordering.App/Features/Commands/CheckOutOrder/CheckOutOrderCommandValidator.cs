﻿using FluentValidation;

namespace Ordering.App.Features.Commands.CheckOutOrder
{
	public class CheckOutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
	{
		public CheckOutOrderCommandValidator()
		{
			RuleFor(p => p.UserName)
				.NotEmpty().WithMessage("{UserName} is required.")
				.NotNull()
				.MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

			RuleFor(p => p.EmailAddress)
				.NotEmpty().WithMessage("{EmailAddress} is required.");
			RuleFor(p => p.TotalPrice)
				.NotEmpty().WithMessage("{TotalPrice} is required.")
				.GreaterThan(0).WithMessage("{TotalPrice} must be greater than zero");


		}
	}
}
