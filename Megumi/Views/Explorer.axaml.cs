using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Megumi.Core.ViewModels;

namespace Megumi.Views;

public partial class Explorer : ReactiveUserControl<ExplorerViewModel>
{
    public Explorer()
    {
        InitializeComponent();
    }
}
