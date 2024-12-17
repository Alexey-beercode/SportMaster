using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class UserRequestDTOValidator : AbstractValidator<UserRequestDTO>
{
    public UserRequestDTOValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Age).InclusiveBetween(1, 120);
        RuleFor(x => x.Height).GreaterThan(0);
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}