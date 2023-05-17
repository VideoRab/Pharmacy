using Entities.DTO;
using FluentValidation;

namespace Common.Validation;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.Medicines)
            .NotEmpty()
            .NotNull()
            .WithMessage("Basket can't not be empty!");
    }
}
