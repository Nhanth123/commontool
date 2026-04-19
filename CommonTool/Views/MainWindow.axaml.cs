using Avalonia.Controls;
using Avalonia.Interactivity;
<<<<<<< HEAD
=======
using CommunityToolkit.Mvvm.Input;
>>>>>>> e3187eb85d6bbf05ee83c80455ac02a4b51a1180

namespace CommonTool.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
<<<<<<< HEAD
    
    public void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        // Your logic here
        var button = sender as Button;
        button.Content = "Clicked!";
    }
=======

    private int _clickCount = 0;

    public void OnClick(object sender, RoutedEventArgs args)
    {
        var btn = (Button) sender;
        btn.Content = $"Clicked: {++_clickCount} times";
    }

>>>>>>> e3187eb85d6bbf05ee83c80455ac02a4b51a1180
}