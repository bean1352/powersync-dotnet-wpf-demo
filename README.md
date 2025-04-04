# PowerSync + WPF Demo: To-Do List

## Overview

This is a demo WPF application that showcases how to use the [PowerSync SDK](https://www.powersync.com) for data synchronization in a to-do list application. The app leverages PowerSync to sync task lists and items while working seamlessly online and offline.

## Getting Started

### Prerequisites

Ensure you have the following installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Installation

Clone this repository and navigate to the project directory:

```sh
git https://github.com/bean1352/powersync-dotnet-wpf-demo.git
cd powersync-dotnet-wpf-demo
```

### Configuration

Copy the example environment file and update it with your PowerSync credentials:

```sh
cp .env.template .env
```

Edit `.env` to include the necessary API keys and connection details.

### Running the App

Build and run the WPF application using the .NET CLI:

```sh
dotnet build
```

Run the app:

```sh
dotnet run
```

Alternatively, open the solution in Visual Studio and start debugging (`F5`).

## Features

- **Offline-first Sync**: PowerSync ensures that tasks and lists are synchronized efficiently when online.
- **Task Management**: Add, edit, complete, and delete tasks within lists.
- **MVVM Architecture**: Clean separation of concerns with ViewModels and Views.
- **Shell Navigation**: `MainWindow` serves as the main navigation shell.

## Learn More

- [PowerSync SDK Documentation](https://docs.powersync.com)
- [PowerSync GitHub Repository](https://github.com/powersync-ja/powersync-js)

Feedback and contributions are welcome!

