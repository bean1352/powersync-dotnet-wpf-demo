using System.Collections.ObjectModel;
using System.Windows.Input;
using PowerSync.Common.Client;
using PowersyncDotnetTodoList.Models;
using PowersyncDotnetTodoList.Services;

namespace PowersyncDotnetTodoList.ViewModels
{
    public class TodoViewModel : ViewModelBase
    {
        // private readonly PowerSyncDatabase _db;
        // private readonly PowerSyncConnector _connector;

        // public ObservableCollection<TodoItem> Todos { get; } = new();

        // private TodoItem? _selectedTodo;
        // public TodoItem? SelectedTodo
        // {
        //     get => _selectedTodo;
        //     set
        //     {
        //         _selectedTodo = value;
        //         OnPropertyChanged();
        //     }
        // }

        // public ICommand AddTodoCommand { get; }
        // public ICommand DeleteTodoCommand { get; }
        // public ICommand MarkCompleteCommand { get; }

        // public TodoViewModel(IPowerSyncDatabase db, PowerSyncConnector connector)
        // {
        //     _db = db as PowerSyncDatabase ?? throw new InvalidCastException("Expected PowerSyncDatabase instance.");
        //     _connector = connector;

        //     AddTodoCommand = new RelayCommand<string>(async (taskName) => await AddTodo(taskName));
        //     DeleteTodoCommand = new RelayCommand<TodoItem>(async (todo) => await DeleteTodo(todo));
        //     MarkCompleteCommand = new RelayCommand<TodoItem>(async (todo) => await MarkComplete(todo));

        //     LoadTodos();
        // }

        // private async void LoadTodos()
        // {
        //     var todos = await _db.GetAll<TodoItem>("SELECT * FROM todos;");
        //     Todos.Clear();
        //     foreach (var todo in todos)
        //     {
        //         Todos.Add(todo);
        //     }
        // }

        // private async Task AddTodo(string taskName)
        // {
        //     if (!string.IsNullOrWhiteSpace(taskName))
        //     {
        //         await _db.Execute(
        //             "INSERT INTO todos (id, name, completed, created_at) VALUES (uuid(), ?, 0, datetime())",
        //             [taskName]
        //         );
        //         LoadTodos();
        //     }
        // }

        // private async Task DeleteTodo(TodoItem todo)
        // {
        //     await _db.Execute("DELETE FROM todos WHERE id = ?", [todo.Id]);
        //     Todos.Remove(todo);
        // }

        // private async Task MarkComplete(TodoItem todo)
        // {
        //     await _db.Execute("UPDATE todos SET completed = 1 WHERE id = ?", [todo.Id]);
        //     todo.Completed = true;
        //     OnPropertyChanged(nameof(Todos));
        // }
    }
}