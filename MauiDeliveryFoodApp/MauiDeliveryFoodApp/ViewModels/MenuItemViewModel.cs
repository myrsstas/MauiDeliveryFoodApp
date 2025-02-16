using System.Windows.Input;
using MauiDeliveryFoodApp.Models;
using MauiDeliveryFoodApp.Services;

namespace MauiDeliveryFoodApp.ViewModels;

public class MenuItemViewModel : BaseViewModel
{
    private string _name, _description, _image;
    private readonly MealPreparationService _mealPreparationService;
    private readonly LastMealService _lastMealService;
    private string _orderButtonLabel;
    private string _cancelButtonLabel;
    private FoodModel _foodModel;
    private ICommand _orderButtonCommand;
    private ICommand _cancelCommand;
    private ICommand _orderReadyCommand;
    private bool _cancelButtonVisibility;
    private bool _orderButtonVisibility;
    private string _processLabelText;
    private bool _processLabelVisibility;
    private int _latestOrderID;
    
    public MenuItemViewModel()
    {
        
    }
    
    public MenuItemViewModel(MealPreparationService mealPreparationService , LastMealService lastMealService, FoodModel foodModel)
    {
        _mealPreparationService = mealPreparationService;
        _lastMealService = lastMealService;
        _foodModel = foodModel;
    }
    
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    public string Description
    {
        get => _description;
        set => SetField(ref _description, value);
    }

    public string Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }
    
    public string CancelButtonLabel
    {
        get => _cancelButtonLabel;
        set => SetField(ref _cancelButtonLabel, value);
    }

    public string OrderButtonLabel
    {
        get => _orderButtonLabel;
        set => SetField(ref _orderButtonLabel, value);
    }
    

    public ICommand ButtonTappedCommand
    {
        get => _orderButtonCommand;
        set => SetField(ref _orderButtonCommand, value);
    }

    public ICommand CancelButtonCommand
    {
        get => _cancelCommand;
        set => SetField(ref _cancelCommand, value);
    }

    public ICommand OrderReadyCommand
    {
        get => _orderReadyCommand;
        set => SetField(ref _orderReadyCommand, value);
    }

    public bool CancelButtonVisibility
    {
        get => _cancelButtonVisibility;
        set => SetField(ref _cancelButtonVisibility, value);
    }

    public bool OrderButtonVisibility
    {
        get => _orderButtonVisibility;
        set => SetField(ref _orderButtonVisibility, value);
    }
    

    public string ProcessLabelText
    {
        get => _processLabelText;
        set => SetField(ref _processLabelText, value);
    }

    public bool ProcessLabelVisibility
    {
        get => _processLabelVisibility;
        set => SetField(ref _processLabelVisibility, value);
    }

    public int LatestOrderID
    {
        get => _latestOrderID;
        set => SetField(ref _latestOrderID, value);
    }

    public FoodModel foodModel { get; set; }
    
    private void ExecuteButtonTappedCommand()
    {
        CancelButtonVisibility = true;
        OrderButtonVisibility = false;
        ProcessLabelText = $"Preparing for {Name}";
        ProcessLabelVisibility = true;
        LatestOrderID = _mealPreparationService.NextOrderId;
        _mealPreparationService.PrepareFood(Name, LatestOrderID).ContinueWith(UpdateOrderStatus);
       
    }

    private void UpdateOrderStatus(Task<bool> task)
    {
        System.Diagnostics.Debug.WriteLine(task.Result);
        if (!task.Result)
        {
            CancelButtonVisibility = false;
            OrderButtonVisibility = true;
            ProcessLabelText = $"Your order for {Name} was cancelled";
            return;
        }

        CancelButtonVisibility = false;
        OrderButtonVisibility = true;
        ProcessLabelText = $"Your {Name} is ready";
        _lastMealService.SaveLastMeal(_foodModel);
    }

    private void CancelButtonTappedCommand()
    {
        System.Diagnostics.Debug.WriteLine("Cancel Command Executed, Hellooooo!");
        _mealPreparationService.CancelMealPreparation(Name, LatestOrderID);
    }
    
    public override void Initialize()
    {
        OrderButtonLabel = "Button Here";
        CancelButtonLabel = "Cancel Here";
        LatestOrderID = -1;
        ButtonTappedCommand = new Command(ExecuteButtonTappedCommand);
        CancelButtonCommand = new Command(CancelButtonTappedCommand);
        OrderButtonVisibility = true;
        CancelButtonVisibility = false;
        ProcessLabelVisibility = false;
    }
    
}