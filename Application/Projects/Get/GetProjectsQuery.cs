using Application.Project.Get;
using Domain.Task;
using MediatR;

namespace Application.Projects;

public record GetProjectsQuery(
    Guid UserId,
    string? SearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IRequest<PagedList<ProjectResponse>>;


public record ProjectResponse(Guid Id, string Name, string Description, DateTime CreateDate, IReadOnlyCollection<TaskDetail> Tasks);
