
using Microsoft.EntityFrameworkCore;

namespace CommonTool.Database;

public class DbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //public DbSet<DailyTask> DailyTasks => Set<DailyTask>();
}