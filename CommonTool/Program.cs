using Avalonia;
using System;
using Serilog;

namespace CommonTool;

sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() // Set minimum severity
            .WriteTo.Console() // Output to console
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        Log.CloseAndFlushAsync(); // Ensure logs are written before exit
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}