using Domain.Extension;
using FluentValidation;

namespace Application.Commands.Resturant.CreateResturant;

public class CreateResturantCommandValidator : AbstractValidator<CreateResturantCommand>
{
    public CreateResturantCommandValidator()
    {
        RuleFor(f => f.ResturantDto.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(f => f.ResturantDto.ResturantTables)
            .NotNull()
            .Must(m => m.Count >= 1)
            .WithMessage("{PropertyName} is required");
        
        RuleFor(f => f.ResturantDto.ResturantOwnerEmail)
            .NotNull()
            .NotEmpty()
            .Must(ResturantExtentions.ValidateEmailAddress)
            .WithMessage("{PropertyName} is required");
    }
}
