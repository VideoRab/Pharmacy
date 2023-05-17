using Entities.DTO;
using FluentValidation;

namespace Common.Validation;

public class MedicineValidator : AbstractValidator<MedicineDto>
{
    public MedicineValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty!")
            .NotNull()
            .WithMessage("Name can't be null!");
        RuleFor(x => x.Type).IsInEnum();
    }
}
