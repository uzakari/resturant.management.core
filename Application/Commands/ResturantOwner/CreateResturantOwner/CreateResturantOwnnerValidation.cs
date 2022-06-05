using Domain.Extension;
using FluentValidation;

namespace Application.Commands.ResturantOwner.CreateResturantOwner;

public class CreateResturantOwnnerValidation : AbstractValidator<CreateResturantOwnerCommand>
{
    public CreateResturantOwnnerValidation()
    {
        RuleFor(f => f.ResturantOwnerDto.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(f => f.ResturantOwnerDto.LastName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(f => f.ResturantOwnerDto.Email)
            .NotEmpty()
            .Must(ResturantExtentions.ValidateEmailAddress)
            .WithMessage("{PropertyName} not valid");
    }
}
