using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Extensions.DependencyInjection;
using PowersyncDotnetTodoList.Services;
using PowersyncDotnetTodoList.ViewModels;
using PowersyncDotnetTodoList.Views;

namespace PowersyncDotnetTodoList;

public partial class MainWindow : Window
{
    // public MainWindow()
    // {
    //     InitializeComponent();
    // }
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        // Set the DataContext to the view model
        this.DataContext = viewModel;
    }
}
