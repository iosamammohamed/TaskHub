using FluentValidation;

namespace TaskHub.Application.Features.WorkItems.Commands.DeleteWorkItem;

public class DeleteWorkItemCommandValidator : AbstractValidator<DeleteWorkItemCommand>
{
    public DeleteWorkItemCommandValidator()
    {
        RuleFor(v => v.Id).NotEmpty();
    }
}
 
