using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class ExerciseLogRequestDTOValidator : AbstractValidator<ExerciseLogRequestDTO>
{
    public ExerciseLogRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.ExerciseType).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Duration).GreaterThan(0);
        RuleFor(x => x.CaloriesBurned).GreaterThanOrEqualTo(0);
    }
}