using FluentValidation;
using SportMaster.BLL.Dtos.Request;

namespace SportMaster.API.Validators;


public class NotificationRequestDTOValidator : AbstractValidator<NotificationRequestDTO>
{
    public NotificationRequestDTOValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Message).NotEmpty().MaximumLength(500);
        RuleFor(x => x.Type).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow);
    }
}