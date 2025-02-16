namespace MauiDeliveryFoodApp.Services;

public class MealPreparationService
{
    private readonly Dictionary<int, CancellationTokenSource> _mealsGettingPrepared =
        new Dictionary<int, CancellationTokenSource>();

    public int NextOrderId = 0;

    public async Task<bool> PrepareFood(string foodName, int orderId)
    {
        var cts = new CancellationTokenSource();
        _mealsGettingPrepared[orderId] = cts;
        UpdateNextOrderId();

        try
        {
            System.Diagnostics.Debug.WriteLine($"I am getting there for food {foodName} with order {NextOrderId}");
            await PrepareFoodAsync(cts.Token);
            return true;
        }
        catch (OperationCanceledException exception)
        {
            System.Diagnostics.Debug.WriteLine("caught you");
            return false;
        }
    }


    private async Task PrepareFoodAsync(CancellationToken ctsToken)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                if (ctsToken.IsCancellationRequested)
                {
                    ctsToken.ThrowIfCancellationRequested();
                }
                Thread.Sleep(1000);
            }
        }, ctsToken);
    }


    public void CancelMealPreparation(string foodName, int orderId)
    {
        if (_mealsGettingPrepared.TryGetValue(orderId, out CancellationTokenSource cancellationTokenSource))
        {
            cancellationTokenSource.Cancel();
            _mealsGettingPrepared.Remove(orderId);
        }

        System.Diagnostics.Debug.WriteLine($"i got cancelled for food {foodName}");
    }

    private void UpdateNextOrderId()
    {
        NextOrderId++;
    }
}