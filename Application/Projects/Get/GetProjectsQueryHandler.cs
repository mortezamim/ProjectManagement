using Application.Data;
using Application.Projects;
using MediatR;
using System.Linq.Expressions;

namespace Application.Project.Get;

internal sealed class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PagedList<ProjectResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetProjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedList<ProjectResponse>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Projects.Project> productsQuery = _context.Projects.Where(x => x.UserId == request.UserId);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            productsQuery = productsQuery.Where(p =>
                p.Name.Contains(request.SearchTerm) ||
                p.Description.Contains(request.SearchTerm)
                );
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            productsQuery = productsQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            productsQuery = productsQuery.OrderBy(GetSortProperty(request));
        }

        var productResponsesQuery = productsQuery
            .Select(p => new ProjectResponse(
                p.Id.Value,
                p.Name,
                p.Description,
                p.CreateDate,
                p.Tasks));

        var products = await PagedList<ProjectResponse>.CreateAsync(
            productResponsesQuery,
            request.Page,
            request.PageSize);


        return products;
    }

    private static Expression<Func<Domain.Projects.Project, object>> GetSortProperty(GetProjectsQuery request) =>
        request.SortColumn?.ToLower() switch
        {
            "name" => product => product.Name,
            "description" => product => product.Description,
            _ => product => product.Id
        };
}
