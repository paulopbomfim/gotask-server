using FluentValidation;
using GoTask.Communication.Requests;

namespace GoTask.Application.UseCases.Task;

public class TaskValidator : AbstractValidator<TaskRequest>
{
    public TaskValidator()
    {
        RuleFor(p => p.Title).NotEmpty();
    }
}