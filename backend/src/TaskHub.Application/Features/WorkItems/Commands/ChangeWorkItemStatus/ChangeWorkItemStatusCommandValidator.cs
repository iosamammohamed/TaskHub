using FluentValidation;

namespace TaskHub.Application.Features.WorkItems.Commands.ChangeWorkItemStatus;

public class ChangeWorkItemStatusCommandValidator : AbstractValidator<ChangeWorkItemStatusCommand>
{
    public ChangeWorkItemStatusCommandValidator()
    {
        RuleFor(v => v.Status).IsInEnum();
    }
}
 
