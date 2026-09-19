using System;
using System.Collections;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using IBM.WMQ;
using Serilog.Core;


namespace CommonTool.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private readonly string _desktopPath = @"C:\\Users\\nhanth37440\\Desktop\\";
    private readonly string _sacomFolderPath = @"C:\\SacomDeploy";
    
    private readonly Hashtable _connectionProperties;
    private readonly string _queueManagerName;
    private readonly string _queueName;
    

    public void OnClick(object sender, RoutedEventArgs args)
    {
        // RemoveDesktopIcon();
        // RemoveSacomDeployFolder();

        SendMessage();
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


    private void UploadMessageToQueue()
    {
        
    }
    
    public void SendMessage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Log.Warning($"File not found: {filePath}");
            return;
        }

        MQQueueManager queueManager = null;
        MQQueue queue = null;

        try
        {
            Log.Information($"Connecting to {_queueManagerName}...");
            queueManager = new MQQueueManager(_queueManagerName, _connectionProperties);

            int openOptions = MQC.MQOO_OUTPUT | MQC.MQOO_FAIL_IF_QUIESCING;
            queue = queueManager.AccessQueue(_queueName, openOptions);

            int messageCount = 0;

            // ReadLines streams the file, which prevents memory spikes on massive files
            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var mqMessage = new MQMessage
                {
                    Format = MQC.MQFMT_STRING
                };
                mqMessage.WriteString(line);

                var putOptions = new MQPutMessageOptions();
                queue.Put(mqMessage, putOptions);
                
                messageCount++;
            }

            Log.Information($"Successfully sent {messageCount} messages to {_queueName}.");
        }
        catch (MQException mqEx)
        {
            Log.Warning($"MQ Error: Reason Code {mqEx.ReasonCode}, Comp Code {mqEx.CompCode}");
        }
        catch (Exception ex)
        {
            Log.Warning($"Application Error: {ex.Message}");
        }
        finally
        {
            // Always ensure the queue and manager are explicitly closed
            queue?.Close();
            queueManager?.Disconnect();
        }
    }
    
}