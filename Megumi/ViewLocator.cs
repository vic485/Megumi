using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Megumi;

/// <summary>
/// Avalonia view locator.
/// </summary>
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var view = ReactiveUI.ViewLocator.Current.ResolveView(param);

        if (view is not null)
        {
            view.ViewModel = param;
            return (Control)view;
        }

        var baseType = param.GetType().BaseType;
        while (baseType is not null)
        {
            var baseView = ReactiveUI.ViewLocator.Current.ResolveView(baseType);
            if (baseView is not null)
            {
                baseView.ViewModel = param;
                return (Control)baseView;
            }

            baseType = baseType.BaseType;
        }

        return new TextBlock
        {
            Text = $"View not found for: {param.GetType().Name}"
        };
    }

    public bool Match(object? data) => data?.GetType().Name.EndsWith("ViewModel") ?? false;
}
