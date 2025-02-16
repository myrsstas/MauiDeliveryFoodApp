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

    public FoodModel foodModel { get; set; }
    
    public ObservableCollection<FoodModel> lastFood { get; set; } = new ObservableCollection<FoodModel>();

    private void LoadLastFoodOrdered()
    {
        if (_lastMealService == null)
        {
            LastOrderName = "No Previous Order";
            LastOrderImage = null;
            LastOrderDescription = null;
            
        }
        foodModel = _lastMealService.ReadLastMeal();

        if (foodModel != null)
        {
            lastFood.Add(foodModel);
            LastOrderName = lastFood.Last().Name;
            LastOrderImage = lastFood.Last().Image;
            LastOrderDescription = lastFood.Last().Description;
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