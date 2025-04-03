using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PowerSync.Common.Client;
using PowersyncDotnetTodoList.Models;
using PowersyncDotnetTodoList.Services;
using PowersyncDotnetTodoList.Views;

namespace PowersyncDotnetTodoList.ViewModels
{
    public class TodoListViewModel : ViewModelBase
    {
        private readonly PowerSyncDatabase _db;
        private readonly PowerSyncConnector _connector;
        private readonly INavigationService _navigationService;

        public ObservableCollection<TodoList> TodoLists { get; } = [];
        private TodoList? _selectedList;

        public TodoList? SelectedList
        {
            get => _selectedList;
            set
            {
                if (_selectedList != value)
                {
                    _selectedList = value;
                    OnPropertyChanged();
                    OpenList(_selectedList);
                }
            }
        }
        private string _newListName;
        public string NewListName
        {
            get => _newListName;
            set
            {
                if (_newListName != value)
                {
                    _newListName = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddListCommand { get; }
        public ICommand DeleteListCommand { get; }

        public TodoListViewModel(
            PowerSyncDatabase db,
            PowerSyncConnector connector,
            INavigationService navigationService
        )
        {
            _db = db;
            _connector = connector;
            _navigationService = navigationService;

            AddListCommand = new RelayCommand<string>(
                async (newListName) =>
                {
                    if (!string.IsNullOrWhiteSpace(newListName))
                    {
                        await AddList(newListName);
                    }
                }
            );

            DeleteListCommand = new RelayCommand<TodoList>(
                async (list) =>
                {
                    if (list != null)
                    {
                        await DeleteList(list);
                    }
                }
            );

            WatchForChanges();
            LoadTodoLists();
        }

        private async Task LoadTodoLists()
        {
            var lists = await _db.GetAll<TodoList>("SELECT * FROM lists;");
            TodoLists.Clear();
            foreach (var list in lists)
            {
                TodoLists.Add(list);
            }
        }

        private async void WatchForChanges()
        {
            await _db.Watch(
                "SELECT * FROM lists",
                null,
                new WatchHandler<TodoList>
                {
                    OnResult = (results) =>
                    {
                        var dispatcher = System.Windows.Application.Current?.Dispatcher;
                        if (dispatcher != null)
                        {
                            dispatcher.Invoke(() =>
                            {
                                TodoLists.Clear();
                                foreach (var result in results)
                                {
                                    TodoLists.Add(result);
                                }
                            });
                        }
                    },
                    OnError = (error) =>
                    {
                        Console.WriteLine("Error: " + error.Message);
                    },
                }
            );
        }

        private async Task AddList(string newListName)
        {
            try
            {
                await _db.Execute(
                    "INSERT INTO lists (id, name, owner_id, created_at) VALUES (uuid(), ?, ?, datetime())",
                    [newListName, _connector!.UserId]
                );
            }
            catch (Exception ex) { }
        }

        private async Task DeleteList(TodoList list)
        {
            await _db.Execute("DELETE FROM lists WHERE id = ?", [list.id]);
            TodoLists.Remove(list);
        }

        private void OpenList(TodoList? selectedList)
        {
            if (selectedList != null)
            {
                // _navigationService.Navigate<TodoViewModel>(selectedList);
            }
        }
    }
}

