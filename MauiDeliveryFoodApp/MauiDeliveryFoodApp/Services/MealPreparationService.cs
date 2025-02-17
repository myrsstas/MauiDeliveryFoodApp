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
            await PrepareFoodAsync(cts.Token);
            return true;
        }
        catch (OperationCanceledException exception)
        {
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
    }

    private void UpdateNextOrderId()
    {
        NextOrderId++;
    }
}