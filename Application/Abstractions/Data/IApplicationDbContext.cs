using Domain.Customers;
using Domain.Orders;
using Domain.Products;
using Domain.Projects;
using Domain.Task;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }

    DbSet<Project> Projects { get; set; }

    DbSet<TaskDetail> Tasks { get; set; }

    DbSet<Customer> Customers { get; set; }

    DbSet<Order> Orders { get; set; }

    DbSet<LineItem> LineItems { get; set; }

    DbSet<Product> Products { get; set; }
}
