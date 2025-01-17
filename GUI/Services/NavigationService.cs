﻿using System;
using GUI.Core;

namespace GUI.Services;

public class NavigationService : ObservableObject, INavigationService
{
    private ViewModel _currentView;
    private readonly Func<Type, ViewModel> _viewModelFactory;

    public ViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;

    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModel
    {
        ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}
