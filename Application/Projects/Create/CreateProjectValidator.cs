using Domain.Projects;
using FluentValidation;

namespace Application.Project.Create;

public sealed class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator(IProjectRepository projectRepository)
    {
        RuleFor(x => x.Name).NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(5)
            .WithMessage("Name minimum length is 5 characters");
    }
}
