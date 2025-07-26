using FluentValidation;
using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.User.Register;

public class RegisterUserValidation : AbstractValidator<UserRequest>
{
    public RegisterUserValidation()
    {
        RuleFor(p => p.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(p => p.Name)
            .NotEmpty();

        RuleFor(p => p.Role)
            .IsInEnum();
        
        RuleFor(p => p.Password).SetValidator(new PasswordValidation<UserRequest>());
    }
}