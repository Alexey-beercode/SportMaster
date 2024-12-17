using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class RecommendationRequestDTOValidator : AbstractValidator<RecommendationRequestDTO>
{
    public RecommendationRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.RecommendationText).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow);
    }
}