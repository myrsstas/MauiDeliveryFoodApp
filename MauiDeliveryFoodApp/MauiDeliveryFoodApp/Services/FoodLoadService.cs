using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using MauiDeliveryFoodApp.Models;

namespace MauiDeliveryFoodApp.Services;

public class FoodLoadService
{
    private readonly string _jsonFilePath = "MauiDeliveryFoodApp.Resources.Data.foodMenu.json";

    public ObservableCollection<FoodModel> GetMenuItems()
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        using Stream stream = asm.GetManifestResourceStream(_jsonFilePath);
        return JsonSerializer.Deserialize<ObservableCollection<FoodModel>>(stream);
    }
    
}