using GoTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoTask.Infrastructure.DataAccess;

public class GoTaskDbContext(DbContextOptions options ) : DbContext(options)
{
   public DbSet<TaskEntity> Tasks { get; init; }
   public DbSet<Comment> Comments { get; init; }
   public DbSet<Organization> Organizations { get; init; }
   public DbSet<User> Users { get; init; }
}