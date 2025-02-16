using System.Text.Json;
using MauiDeliveryFoodApp.Models;

namespace MauiDeliveryFoodApp.Services;

public class LastMealService
{
    private string _filePath = Path.Combine(FileSystem.AppDataDirectory, "History");
    private string _completePath;
    private string _fileName = "last_meal.json";


    public LastMealService()
    {
        _completePath = Path.Combine(_filePath, _fileName);
    }

    public void SaveLastMeal(FoodModel foodModel)
    {
        try
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            var jsonString = JsonSerializer.Serialize(foodModel);

            File.WriteAllText(_completePath, jsonString);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
        }
    }

    public FoodModel ReadLastMeal()
    {
        if (!HasLastMeal())
        {
            return null;
        }

        var jsonString = File.ReadAllText(_completePath);
        var foodModel = JsonSerializer.Deserialize<FoodModel>(jsonString);

        return foodModel;
    }

    public bool HasLastMeal()
    {
        return File.Exists(_completePath);
    }
}