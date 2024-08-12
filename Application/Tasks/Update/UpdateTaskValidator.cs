using Domain.Projects;
using FluentValidation;

namespace Application.TaskDetails.Update;

public sealed class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(5)
            .WithMessage("Name minimum length is 5 characters");

        RuleFor(x => x.Status)
            .ExclusiveBetween((byte)1, (byte)3)
            .WithMessage("Status must be between 1 and 3");
    }
}
