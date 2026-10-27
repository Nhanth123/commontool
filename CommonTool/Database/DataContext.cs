
using Microsoft.EntityFrameworkCore;

namespace CommonTool.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //public DbSet<DailyTask> DailyTasks => Set<DailyTask>();
}