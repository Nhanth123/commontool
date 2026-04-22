using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace CommonTool.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private string _desktopPath = @"C:\\Users\\nhanth37440\\Desktop\\";
    private string _sacomFolderPath = @"C:\\SacomDeploy";

    public void OnClick(object sender, RoutedEventArgs args)
    {
        RemoveDesktopIcon();
        RemoveSacomDeployFolder();
    }

    private void AppAbout_OnClick(object? sender, System.EventArgs args) {

    }

    private void AppPreferences_OnClick(object? sender, System.EventArgs args) {
    
    }

    private void RemoveDesktopIcon()
    {
        Log.Information("Desktop path: {Path}", _desktopPath);

        if (string.IsNullOrEmpty(_desktopPath) || !Directory.Exists(_desktopPath))
        {
            Log.Warning("Desktop path is invalid");
            return;
        }

        foreach (var file in Directory.GetFiles(_desktopPath, "*.url"))
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

    private void RemoveSacomDeployFolder()
    {
        
        Log.Information("Desktop path: {Path}", _sacomFolderPath);

        try
        {
            if (Directory.Exists(_sacomFolderPath))
            {
                Directory.Delete(_sacomFolderPath, true); // true = recursive delete
                Log.Information($"Removed folder: {_sacomFolderPath}");
            }
            else
            {
               Log.Error($"Folder does not exist: {_sacomFolderPath}");
            }
        }
        catch (IOException e)
        {
            Log.Error($"IO Error: {e.Message}");
        }
        catch (UnauthorizedAccessException e)
        {
            Log.Error($"Access Error: {e.Message}");
        }
        catch (Exception e)
        {
            Log.Error($"Unexpected Error: {e.Message}");
        }
    }
}