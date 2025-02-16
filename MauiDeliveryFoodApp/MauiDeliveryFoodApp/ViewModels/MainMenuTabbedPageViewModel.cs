using System.Collections.ObjectModel;
using MauiDeliveryFoodApp.Models;
using MauiDeliveryFoodApp.Services;

namespace MauiDeliveryFoodApp.ViewModels;

public class MainMenuTabbedPageViewModel : BaseViewModel
{
    private MealPreparationService _mealPreparationService;
    private FoodLoadService _foodLoadService;
    private LastMealService _lastMealService;

    public MainMenuTabbedPageViewModel()
    {
    }

    public ObservableCollection<MenuItemViewModel> MenuItemViewModelList { get; set; }
    public ObservableCollection<FoodModel> foodListObservableCollection { get; set; }
    
    public override void Initialize()
    {
        _mealPreparationService = new MealPreparationService();
        _foodLoadService = new FoodLoadService();
        _lastMealService = new LastMealService();
        
        foodListObservableCollection = _foodLoadService.GetMenuItems();
        MenuItemViewModelList = new ObservableCollection<MenuItemViewModel>();
        foreach (var foodModel in foodListObservableCollection)
        {
            MenuItemViewModelList.Add(GetViewModelFromFoodModel(foodModel));
        }
    }
    
    private MenuItemViewModel GetViewModelFromFoodModel(FoodModel foodModel)
    {
        return new MenuItemViewModel(_mealPreparationService, _lastMealService, foodModel)
        {
            Name = foodModel.Name,
            Description = foodModel.Description,
            Image = foodModel.Image
        };
    }

    public void ClearLabels()
    {
        foreach (var item in MenuItemViewModelList)
        {
            item.ClearStatus();
        }
    }
    
}