using Domain.Primitives;
using Domain.Products;

namespace Domain.Projects;

public record ProjectCreatedDomainEvent(Guid Id, ProjectId ProjectId) : DomainEvent(Id);
