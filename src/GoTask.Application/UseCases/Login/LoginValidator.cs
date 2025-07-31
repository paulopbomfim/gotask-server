using FluentValidation;
using GoTask.Communication.Requests;
using GoTask.Exceptions;

namespace GoTask.Application.UseCases.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage(ErrorMessagesResource.EMAIL_EMPTY);
        RuleFor(x => x.Password).NotEmpty().WithMessage(ErrorMessagesResource.PASSWORD_EMPTY);
    }
}