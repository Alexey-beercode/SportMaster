using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class FoodLogRequestDTOValidator : AbstractValidator<FoodLogRequestDTO>
{
    public FoodLogRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.MealType).NotEmpty().MaximumLength(50);
        RuleFor(x => x.FoodName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Calories).GreaterThan(0);
        RuleFor(x => x.Protein).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Carbs).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Fat).GreaterThanOrEqualTo(0);
    }
}
