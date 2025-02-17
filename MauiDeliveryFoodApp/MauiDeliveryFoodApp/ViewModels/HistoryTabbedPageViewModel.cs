using System.Collections.ObjectModel;
using MauiDeliveryFoodApp.Models;
using MauiDeliveryFoodApp.Services;

namespace MauiDeliveryFoodApp.ViewModels;

public class HistoryTabbedPageViewModel : BaseViewModel
{
    private string _lastOrderName;
    private string _lastOrderImage;
    private string _lastOrderDescription;
    private FoodModel _foodModel;
    private LastMealService _lastMealService;

    public HistoryTabbedPageViewModel()
    {
        _lastMealService = new LastMealService();
        Initialize();
    }
    
    public string LastOrderName
    {
        get => _lastOrderName;
        set => SetField(ref _lastOrderName, value);
    }
    
    public string LastOrderImage
    {
        get => _lastOrderImage;
        set => SetField(ref _lastOrderImage, value);
    }
    
    public string LastOrderDescription
    {
        get => _lastOrderDescription;
        set => SetField(ref _lastOrderDescription, value);
    }

    public FoodModel FoodModel { get; set; } = new FoodModel();
    
    public ObservableCollection<FoodModel> LastFood { get; set; } = new ObservableCollection<FoodModel>();

    private void LoadLastFoodOrdered()
    {
        if (_lastMealService == null)
        {
            LastOrderName = "No Previous Order";
            LastOrderImage = null;
            LastOrderDescription = null;
            return;
        }
        FoodModel = _lastMealService.ReadLastMeal();

        if (FoodModel != null)
        {
            LastFood.Add(FoodModel);
            LastOrderName = LastFood.Last().Name;
            LastOrderImage = LastFood.Last().Image;
            LastOrderDescription = LastFood.Last().Description;
        }
        
    }
    
    public override void Initialize()
    {
        LoadLastFoodOrdered();
    }

    public void RefreshLastFoodOrder()
    {
        LoadLastFoodOrdered();
    }
    
}