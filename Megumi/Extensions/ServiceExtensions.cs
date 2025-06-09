namespace Megumi.Extensions;

public static class ServiceExtensions
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
    }
}
