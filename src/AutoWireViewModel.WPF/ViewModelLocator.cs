using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;

namespace AutoWireViewModel.WPF;

public class ViewModelLocator
{
    private readonly IServiceProvider _serviceProvider;

    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached("AutoWireViewModel",
            typeof(bool), typeof(ViewModelLocator),
            new PropertyMetadata(false, AutoWireViewModelChanged));

    public ViewModelLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        if(Application.Current != null && !Application.Current.Resources.Contains("ViewModelLocator"))
        {
            Application.Current.Resources.Add("ViewModelLocator", this);
        }
    }

    public static bool GetAutoWireViewModel(DependencyObject obj)
        => (bool)obj.GetValue(AutoWireViewModelProperty);

    public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        => obj.SetValue(AutoWireViewModelProperty, value);

    private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if(DesignerProperties.GetIsInDesignMode(d)) return;

        var locator = (ViewModelLocator?)((FrameworkElement)d).TryFindResource("ViewModelLocator");
        var viewModel = locator?.LocateViewModelForView(d);
        ((FrameworkElement)d).DataContext = viewModel;
    }

    public object? LocateViewModelForView(object view)
    {
        var viewType = view.GetType();
        var viewNamespace = viewType.Namespace ?? throw new InvalidOperationException($"The type '{viewType}' lacks a namespace.");

        // Ensure the namespace convention for the view is followed.
        if(!viewNamespace.EndsWith(".Views"))
            throw new InvalidOperationException($"The namespace for type '{viewType}' must end in '.Views'.");

        // Derive ViewModel's namespace and name based on the conventions.
        // TODO this is not very readable.
        var viewModelNamespace = $"{viewNamespace[..^6]}.ViewModels"; // "..^6" removes ".Views" from the end.
        var viewModelName = viewType.Name + "ViewModel";

        // MainView.xaml -> MainViewModel not MainViewViewModel
        var viewModelTypeName = $"{viewModelNamespace}.{viewModelName}".Replace("ViewView", "View");
        var viewModelType = Type.GetType($"{viewModelTypeName}, {Assembly.GetAssembly(viewType)}")
            ?? throw new InvalidOperationException($"Cannot locate ViewModel type for {viewType.FullName}.");

        return _serviceProvider.GetService(viewModelType);
    }
}
