using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
namespace CommonTool.Database;

public static class DatabaseFactory
{
    private static readonly string DatabaseDirectory =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CommonTool");

    private static readonly string DatabasePath = Path.Combine(DatabaseDirectory, "CommonTool.db");

    public static AppDbContext CreateContext()
    {
        Directory.CreateDirectory(DatabaseDirectory);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite($"Data Source={DatabasePath}")
            .Options;

        return new AppDbContext(options);
    }
}