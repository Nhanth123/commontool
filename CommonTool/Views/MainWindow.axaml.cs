using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CommonTool.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    public void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        // Your logic here
        var button = sender as Button;
        button.Content = "Clicked!";
    }
}