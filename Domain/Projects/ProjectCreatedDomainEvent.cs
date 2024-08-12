using Domain.Primitives;

namespace Domain.Projects;

public record ProjectCreatedDomainEvent(Guid Id, ProjectId ProjectId) : DomainEvent(Id);
