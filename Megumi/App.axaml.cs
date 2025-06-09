using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Megumi.Core.Extensions;
using Megumi.Extensions;
using Megumi.Views;

namespace Megumi;

public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.RegisterLogging();
        collection.RegisterCoreServices();
        collection.AddViewModels();

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        _serviceProvider = collection.BuildServiceProvider();

        _serviceProvider.GetRequiredService<IDatabaseService>().Initialize();
        var vm = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
            desktop.Exit += (_, _) => OnExit();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnExit() => _serviceProvider.GetRequiredService<IDatabaseService>().SaveConfig();
}
