using MauiDeliveryFoodApp.ViewModels;

namespace MauiDeliveryFoodApp.Views;

public partial class HistoryTabbedPage : ContentPage
{
    public HistoryTabbedPage()
    {
        InitializeComponent();
        BindingContext = new HistoryTabbedPageViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is HistoryTabbedPageViewModel viewModel)
        {
            viewModel.RefreshLastFoodOrder();
        }
    }
}