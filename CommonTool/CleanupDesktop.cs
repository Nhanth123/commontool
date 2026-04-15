namespace CommonTool;

using System;
using System.IO;
using System.Linq;
using Serilog;


public class CleanupDesktop
{
    
    string desktopPath = @"C:\\Users\\nhanth37440\\Desktop\\";
    string sacom_folder_path = @"C:\\SacomDeploy";
    
    void RemoveDesktopIcon()
    {
      
        Log.Information("Desktop path: {Path}", desktopPath);

        if (string.IsNullOrEmpty(desktopPath) || !Directory.Exists(desktopPath))
        {
            Log.Warning("Desktop path is invalid");
            return;
        }

        foreach (var file in Directory.GetFiles(desktopPath, "*.url"))
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