using System.Diagnostics;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PowerSync.Common.Client;
using PowersyncDotnetTodoList.Models;
using PowersyncDotnetTodoList.Services;
using PowersyncDotnetTodoList.ViewModels;
using PowersyncDotnetTodoList.Views;

namespace PowersyncDotnetTodoList
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();
            ConfigureServices(services);

            // Build the service provider
            Services = services.BuildServiceProvider();

            // Initialize the database and connector
            var db = Services.GetRequiredService<PowerSyncDatabase>();
            var connector = Services.GetRequiredService<PowerSyncConnector>();
            await db.Init();
            await db.Connect(connector);
            await db.WaitForFirstSync();

            // // Resolve and show MainWindow
            // var mainWindow = Services.GetRequiredService<MainWindow>();
            // mainWindow.Show();
            var mainWindow = Services.GetRequiredService<MainWindow>();

            // Navigate to initial view
            var navigationService = Services.GetRequiredService<INavigationService>();
            navigationService.Navigate<TodoListViewModel>();

            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Register PowerSyncDatabase
            services.AddSingleton<PowerSyncDatabase>(sp =>
            {
                //var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("PowerSyncLogger");
                return new PowerSyncDatabase(
                    new PowerSyncDatabaseOptions
                    {
                        Database = new SQLOpenOptions { DbFilename = "example.db" },
                        Schema = AppSchema.PowerSyncSchema,
                        //Logger = logger,
                    }
                );
            });

            // Register IPowerSyncDatabase explicitly
            services.AddSingleton<IPowerSyncDatabase>(sp =>
                sp.GetRequiredService<PowerSyncDatabase>()
            );

            // Register PowerSyncConnector
            services.AddSingleton<PowerSyncConnector>();

            // Register ViewModels and Views
            services.AddTransient<TodoListViewModel>();
            services.AddTransient<TodoViewModel>();
            services.AddTransient<TodoListView>();
            services.AddTransient<TodoView>();
            services.AddTransient<MainWindowViewModel>();


            // services.AddSingleton<TodoList>();
            // services.AddSingleton<Todo>();

            // Register MainWindow as a singleton
            services.AddSingleton<MainWindow>();
            // services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<INavigationService>(sp =>
            {
                var mainWindow = sp.GetRequiredService<MainWindow>();
                return new NavigationService(mainWindow.MainFrame, sp);
            });
        }
    }
}
