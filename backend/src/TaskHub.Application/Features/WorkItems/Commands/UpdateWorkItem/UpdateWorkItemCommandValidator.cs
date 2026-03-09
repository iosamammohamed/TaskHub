using FluentValidation;

namespace TaskHub.Application.Features.WorkItems.Commands.UpdateWorkItem;

public class UpdateWorkItemCommandValidator : AbstractValidator<UpdateWorkItemCommand>
{
    public UpdateWorkItemCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");

        RuleFor(v => v.Priority).IsInEnum();
        RuleFor(v => v.Status).IsInEnum();
    }
}
