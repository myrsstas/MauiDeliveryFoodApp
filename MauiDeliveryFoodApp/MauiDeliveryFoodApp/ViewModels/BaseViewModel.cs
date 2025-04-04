﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiDeliveryFoodApp.ViewModels;

public abstract class BaseViewModel : IViewModel
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    public abstract void Initialize();
    
    protected BaseViewModel()
    {
        Initialize();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}