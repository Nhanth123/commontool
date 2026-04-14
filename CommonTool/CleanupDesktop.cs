namespace CommonTool;

using System;
using System.IO;
using System.Linq;

// using DotNetEnv;
// using Serilog;

public class CleanupDesktop
{
    
    string desktopPath = @"C:\\Users\\nhanth37440\\Desktop\\";
    string sacom_folder_path = @"C:\\SacomDeploy";
    
    void RemoveDesktopIcon()
    {
      
        //Log.Information("Desktop path: {Path}", desktopPath);

        if (string.IsNullOrEmpty(desktopPath) || !Directory.Exists(desktopPath))
        {
            //Log.Warning("Desktop path is invalid");
            return;
        }

        foreach (var file in Directory.GetFiles(desktopPath, "*.url"))
        {
            try
            {
                File.Delete(file);
                Console.WriteLine("Removed: {File}", Path.GetFileName(file));
                //Log.Information("Removed: {File}", Path.GetFileName(file));
            }
            catch (IOException ex)
            {
                //Log.Error(ex, "Error removing file: {File}", file);
                Console.WriteLine(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}