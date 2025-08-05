using FluentValidation;
using GoTask.Communication.Requests;
using GoTask.Exceptions;

namespace GoTask.Application.UseCases.User.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ErrorMessagesResource.EMAIL_INVALID);
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage(ErrorMessagesResource.NAME_EMPTY);

        RuleFor(p => p.Role)
            .IsInEnum()
            .WithMessage(ErrorMessagesResource.ROLE_MUST_BE_TYPE);
        
        
        RuleFor(p => p.Password)
            .SetValidator(new PasswordValidation<UserRequest>())
            .Unless(p => string.IsNullOrWhiteSpace(p.Password));
    }
}