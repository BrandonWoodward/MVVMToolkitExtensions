using MVVMToolkitExtensions.MAUI.Interfaces;

namespace MVVMToolkitExtensions.MAUI.Extensions;

public static class MauiAppBuilderExtensions
{
    /// <summary>
    /// Configures the application to use the specified main page as the root for page-based navigation.
    /// </summary>
    /// <typeparam name="TPage">The type of the page that the application starts from.</typeparam>
    /// <param name="builder">The <see cref="MauiAppBuilder"/> to configure.</param>
    /// <returns>
    /// A modified <see cref="MauiAppBuilder"/> that includes a factory function for creating a view 
    /// with its BindingContext set to the ViewModel associated with the provided view type. This 
    /// factory function should be injected into your App.xaml.cs.
    /// </returns>
    /// <remarks>
    /// This method registers a singleton factory function that resolves the necessary services 
    /// to create the specified main page and wraps it in a <see cref="NavigationPage"/> to support 
    /// page-based navigation.
    /// </remarks>
    /// <example>
    /// In your MauiProgram.cs:
    /// <code><![CDATA[
    /// builder
    ///    .UseMauiApp<App>()
    ///    .WithMainPage<MainPage>()
    ///    .Build();
    /// ]]></code>
    /// In your App.xaml.cs:
    /// <code><![CDATA[
    /// public partial class App : Application
    /// {
    ///    public App(Func<Page> mainPage)
    ///    {
    ///        InitializeComponent();
    ///        MainPage = mainPage();
    ///    }
    /// }
    /// ]]></code>
    /// </example>
    public static MauiAppBuilder WithMainPage<TPage>(this MauiAppBuilder builder)
        where TPage : Page
    {
        builder.Services.AddSingleton<Func<Page>>(services =>
        {
            return () =>

            {
                services.GetRequiredService<IViewRegistry>();
                services.GetRequiredService<IRegionRegistry>();

                var pageFactory = services.GetRequiredService<IPageFactory>();
                var (view, _) = pageFactory.Create<TPage>();
                return new NavigationPage(view);
            };
        });

        return builder;
    }
}