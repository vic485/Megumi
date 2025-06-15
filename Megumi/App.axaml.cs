using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Megumi.Core.Extensions;
using Megumi.Extensions;
using Megumi.Views;
using Microsoft.Extensions.Hosting;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace Megumi;

public partial class App : Application
{
    private IHost _host;

    public IServiceProvider Services => _host.Services;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var hostBuilder = new HostBuilder().ConfigureServices((context, services) =>
        {
            services.UseMicrosoftDependencyResolver();
            var resolver = Locator.CurrentMutable;
            resolver.InitializeSplat();

            resolver.RegisterConstant(new AvaloniaActivationForViewFetcher(), typeof(IActivationForViewFetcher));
            resolver.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

            services.RegisterLogging();
            services.RegisterCoreServices();
            services.AddViews();
            services.AddViewModels();
        });

        _host = hostBuilder.Build();
        _host.Services.UseMicrosoftDependencyResolver();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            Services.GetService<IDatabaseService>()!.Initialize();
            var window = new MainWindow();
            var vm = Services.GetService<Megumi.Core.ViewModels.MainWindowViewModel>();
            window.DataContext = vm;

            desktop.MainWindow = window;
            desktop.MainWindow.Show();

            desktop.Exit += async (_, _) =>
            {
                using (_host)
                {
                    Services.GetService<IDatabaseService>()?.SaveConfig();
                    await _host.StopAsync();
                }
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
