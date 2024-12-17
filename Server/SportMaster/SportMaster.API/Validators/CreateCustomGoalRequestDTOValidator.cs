using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class CreateCustomGoalRequestDTOValidator : AbstractValidator<CreateCustomGoalRequestDTO>
{
    public CreateCustomGoalRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.GoalName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.TargetValue).GreaterThan(0);
        RuleFor(x => x.CurrentValue).GreaterThanOrEqualTo(0);
    }
}