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

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            // Handle exceptions from Tasks
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            // Handle exceptions from other threads
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
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
            // services.AddTransient<TodoViewModel>();
            services.AddTransient<TodoListView>();
            // services.AddTransient<TodoView>();

            // Register MainWindow as a singleton
            services.AddSingleton<MainWindow>();

            services.AddSingleton<INavigationService>(sp =>
            {
                var mainWindow = sp.GetRequiredService<MainWindow>();
                return new NavigationService(mainWindow.MainFrame, sp);
            });
        }

        private void App_DispatcherUnhandledException(
            object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e
        )
        {
            HandleException("UI Thread Exception", e.Exception);
            e.Handled = true; // Mark as handled to prevent application crash
        }

        private void TaskScheduler_UnobservedTaskException(
            object sender,
            UnobservedTaskExceptionEventArgs e
        )
        {
            HandleException("Background Task Exception", e.Exception);
            e.SetObserved(); // Mark as observed to prevent application crash
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException("Critical Exception", e.ExceptionObject as Exception);
            // Cannot mark as handled - application will likely terminate
        }

        private void HandleException(string source, Exception ex)
        {
            // Log the exception
            Debug.WriteLine($"{source}: {ex.Message}");

            // You can log to file, database, or error reporting service here

            // Show a user-friendly message
            MessageBox.Show(
                $"An error occurred: {ex.Message}\n\nPlease contact support if this issue persists.",
                "Application Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );

            // Additional handling like:
            // - Send error to telemetry service
            // - Restart application if needed
            // - Collect diagnostic information
        }
    }
}
