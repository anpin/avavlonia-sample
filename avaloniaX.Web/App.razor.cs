using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;

namespace avaloniaX.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        WebAppBuilder.Configure<avaloniaX.App>()
            .UseReactiveUI()
            .SetupWithSingleViewLifetime();
    }
}