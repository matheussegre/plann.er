using Microsoft.EntityFrameworkCore;
using Planner.Infrastructure.Entities;

namespace Planner.Infrastructure;
public class PlannerDbContext: DbContext
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=D:\\Dev\\_Rocketseat\\NLW\\nlw-journey\\plann.er\\backend\\PlannerDatabase.db");
    }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);
    //    modelBuilder.Entity<Activity>().ToTable("Activities");
    //}
}
