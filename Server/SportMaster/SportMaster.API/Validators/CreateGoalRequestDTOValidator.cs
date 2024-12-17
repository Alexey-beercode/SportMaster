using FluentValidation;
using SportMaster.BLL.Dtos.Request;
using SportMaster.Domain.Enums;

namespace SportMaster.API.Validators;

public class CreateGoalRequestDTOValidator : AbstractValidator<CreateGoalRequestDTO>
{
    public CreateGoalRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.GoalType).NotEmpty().IsEnumName(typeof(GoalType));
        RuleFor(x => x.TargetWeight).GreaterThanOrEqualTo(0).When(x => x.TargetWeight.HasValue);
        RuleFor(x => x.DailyCalorieIntake).GreaterThan(0);
        RuleFor(x => x.DailyCalorieBurn).GreaterThanOrEqualTo(0);
    }
}