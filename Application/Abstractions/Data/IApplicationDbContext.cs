using Domain.TaskDetails;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Domain.Projects.Project> Projects { get; set; }

    DbSet<TaskDetail> Tasks { get; set; }

}
