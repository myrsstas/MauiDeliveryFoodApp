using System.ComponentModel;

namespace MauiDeliveryFoodApp.ViewModels;

public interface IViewModel : INotifyPropertyChanged
{
    void Initialize();
}