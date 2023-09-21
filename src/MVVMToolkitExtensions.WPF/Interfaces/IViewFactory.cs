﻿using System.Windows;

namespace MVVMToolkitExtensions.WPF.Interfaces;

public interface IViewFactory
{
    (TView View, object ViewModel) Create<TView>() where TView : FrameworkElement;
}