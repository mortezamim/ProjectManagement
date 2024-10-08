﻿using Domain.Projects;

namespace Domain.Exceptions;

public sealed class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(ProjectId id)
        : base($"The project with the ID = {id.Value} was not found")
    {
    }
}
