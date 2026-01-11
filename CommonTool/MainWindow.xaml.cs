using System.Windows;
using Avalonia.Interactivity;
using System.Diagnostics;

namespace CommonTool;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender,   System.Windows.RoutedEventArgs e)
    {
        Debug.WriteLine("Click!");
    }
}