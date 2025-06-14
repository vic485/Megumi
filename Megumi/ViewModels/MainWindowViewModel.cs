using System.Collections.ObjectModel;
using Megumi.Core.Models;

namespace Megumi.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    
    public int WindowHeight { get => _database.Config.WindowHeight; set => _database.Config.WindowHeight = value; }
    public int WindowWidth { get => _database.Config.WindowWidth; set => _database.Config.WindowWidth = value; }
    
    private readonly IDatabaseService _database;

    public ObservableCollection<FileSystemObject> Items { get; } = [];

    public MainWindowViewModel()
    {
    }

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IDatabaseService database)
    {
        _database = database;
    }
}
