using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AutoWireViewModel.WPF;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterViewModels(this IServiceCollection services, Assembly viewModelAssembly)
    {
        foreach(var type in viewModelAssembly.GetTypes())
        {
            if(!type.IsClass || type.IsAbstract || type.IsGenericType) continue;
            if(type.Namespace == null || !type.Namespace.EndsWith(".ViewModels")) continue;
            if(!type.Name.EndsWith("ViewModel")) continue;

            services.AddTransient(type);
        }

        return services;
    }
}
