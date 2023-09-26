using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder WithMainPage<TPage>(this MauiAppBuilder builder) 
        where TPage : Page
    {
        builder.Services.AddSingleton<Func<Page>>(services =>
        {
            return () =>
            {
                var pageFactory = services.GetRequiredService<IPageFactory>();
                var (view, _) = pageFactory.Create<TPage>();
                return new NavigationPage(view);
            };
        });

        return builder;
    }
}