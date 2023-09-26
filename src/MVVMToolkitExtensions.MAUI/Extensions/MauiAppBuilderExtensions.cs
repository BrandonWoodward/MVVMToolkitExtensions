using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Extensions;

public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Bootstrap the application with a main page.
    /// This page will act as the root for page-based navigation.
    /// </summary>
    /// <typeparam name="TPage">The page your application starts from.</typeparam>
    /// <returns>A factory function for creating a View with the BindingContext set to the
    /// ViewModel registered for the provided view. You should inject this into your App.xaml.cs.
    /// </returns>
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