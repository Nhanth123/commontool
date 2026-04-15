using System.Windows;
using Avalonia.Interactivity;
using System.Diagnostics;
using Serilog;
using System.IO;
using System.Linq;


namespace CommonTool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, System.Windows.RoutedEventArgs e)
    {
        Log.Debug("Button_OnClick");
        
        string desktopPath = @"C:\\Users\\nhanth37440\\Desktop\\";
        string sacom_folder_path = @"C:\\SacomDeploy";
         
        Log.Information("Desktop path: {Path}", desktopPath);

        if (string.IsNullOrEmpty(desktopPath) || !Directory.Exists(desktopPath))
        {
            Log.Warning("Desktop path is invalid");
            return;
        }

        foreach (var file in Directory.GetFiles(sacom_folder_path, "*.url"))
        {
            try
            {
                File.Delete(file);
                Log.Information("Removed: {File}", Path.GetFileName(file));
            }
            catch (IOException ex)
            {
                Log.Error(ex, "Error removing file: {File}", file);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error(ex, "UnauthorizedAccess exception: {File}", file);
            }
        }
    }
}