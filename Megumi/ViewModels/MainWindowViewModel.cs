namespace Megumi.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";
    
    public int WindowHeight { get => _database.Config.WindowHeight; set => _database.Config.WindowHeight = value; }
    public int WindowWidth { get => _database.Config.WindowWidth; set => _database.Config.WindowWidth = value; }
    
    private IDatabaseService _database;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IDatabaseService database)
    {
        _database = database;
    }
}
