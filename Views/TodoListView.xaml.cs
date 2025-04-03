using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerSync.Common.Client;
using PowersyncDotnetTodoList.Models;
using PowersyncDotnetTodoList.Services;
using PowersyncDotnetTodoList.ViewModels;

namespace PowersyncDotnetTodoList.Views
{
    public partial class TodoListView : Page
    {
        public TodoListView()
        {
            InitializeComponent();
        }
    }
}
