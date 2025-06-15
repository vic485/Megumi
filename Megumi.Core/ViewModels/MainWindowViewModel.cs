namespace Megumi.Core.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Control ViewModels

    private ExplorerViewModel _explorerViewModel;

    public ExplorerViewModel ExplorerViewModel
    {
        get => _explorerViewModel;
        set => this.RaiseAndSetIfChanged(ref _explorerViewModel, value);
    }

    #endregion

    public int WindowHeight
    {
        get => _database.Config.WindowHeight;
        set => _database.Config.WindowHeight = value;
    }

    public int WindowWidth
    {
        get => _database.Config.WindowWidth;
        set => _database.Config.WindowWidth = value;
    }

    private IDatabaseService _database;
    private ILogger<MainWindowViewModel> _logger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IDatabaseService database)
    {
        _database = database;
        _logger = logger;
        _explorerViewModel = new ExplorerViewModel();
    }
}
