﻿using System.Windows;
using MVVMToolkitExtensions.Core.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface IRegionManager
{
    void Navigate<TView>(string regionName, NavigationParameters parameters) where TView : FrameworkElement;
    void Clear(string regionName);
}