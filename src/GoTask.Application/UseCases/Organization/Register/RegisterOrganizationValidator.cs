using FluentValidation;
using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.Organization.Register;

public class RegisterOrganizationValidator : AbstractValidator<OrganizationRequest>
{
    public RegisterOrganizationValidator()
    {
        RuleFor(p => p.UserIdentifier)
            .NotEmpty()
            //TODO: Alterar para resource
            .WithMessage("User identifier is required");
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}