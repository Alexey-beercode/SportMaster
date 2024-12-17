using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;

public class ActionHistoryRequestDTOValidator : AbstractValidator<ActionHistoryRequestDTO>
{
    public ActionHistoryRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ActionType).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ActionDate).LessThanOrEqualTo(DateTime.UtcNow);
        RuleFor(x => x.Description).MaximumLength(500);
    }
}