using FluentValidation;
using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.Organization;

public class OrganizationValidator : AbstractValidator<OrganizationRequest>
{
    public OrganizationValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Name is required");
    }
}