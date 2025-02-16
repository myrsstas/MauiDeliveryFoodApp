using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiDeliveryFoodApp.ViewModels;

namespace MauiDeliveryFoodApp.Views;

public partial class MainMenuTabbedPage : ContentPage
{
    public MainMenuTabbedPage()
    {
        InitializeComponent();
        
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MainMenuTabbedPageViewModel viewModel)
        {
            viewModel.ClearLabels();
        }
    }
}