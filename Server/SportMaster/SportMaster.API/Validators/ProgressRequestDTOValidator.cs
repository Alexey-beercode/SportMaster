using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class ProgressRequestDTOValidator : AbstractValidator<ProgressRequestDTO>
{
    public ProgressRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Weight).GreaterThan(0);
        RuleFor(x => x.CaloriesConsumed).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CaloriesBurned).GreaterThanOrEqualTo(0);
    }
}