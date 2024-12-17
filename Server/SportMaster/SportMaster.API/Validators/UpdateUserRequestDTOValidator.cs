using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class UpdateUserRequestDTOValidator : AbstractValidator<UpdateUserRequestDTO>
{
    public UpdateUserRequestDTOValidator()
    {
        RuleFor(x => x.Age).InclusiveBetween(1, 120);
        RuleFor(x => x.Height).GreaterThan(0);
        RuleFor(x => x.Weight).GreaterThan(0);
    }
}
