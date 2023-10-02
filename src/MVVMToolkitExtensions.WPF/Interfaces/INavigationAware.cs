﻿using MVVMToolkitExtensions.WPF.Models;

namespace MVVMToolkitExtensions.WPF.Interfaces;

/// <summary>
/// Tap into the navigation lifecycle of the view/page.
/// </summary>
public interface INavigationAware
{
    /// <summary>
    /// Called when the view/page is navigated to.
    /// </summary>
    /// <param name="parameters">Key/Value pairs to pass to the ViewModel</param>
    void OnNavigatedTo(NavigationParameters parameters);

    /// <summary>
    /// Called when the view/page is navigated from.
    /// </summary>
    void OnNavigatedFrom();
}