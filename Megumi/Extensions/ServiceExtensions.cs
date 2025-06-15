using System.Reflection;

namespace Megumi.Extensions;

public static class ServiceExtensions
{
    public static void AddViews(this IServiceCollection services)
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
    }
}
