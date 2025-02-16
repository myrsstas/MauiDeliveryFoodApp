
using System.Windows.Input;

namespace MauiDeliveryFoodApp.Controls;

public partial class CustomButton : Border
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title),
            typeof(string),
            typeof(CustomButton));

    public static readonly BindableProperty TappedCommandProperty =
        BindableProperty.Create(nameof(TappedCommand),
            typeof(ICommand),
            typeof(CustomButton));

    public CustomButton()
    {
        InitializeComponent();
    }
    
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ICommand TappedCommand
    {
        get=> (ICommand)GetValue(TappedCommandProperty);
        set => SetValue(TappedCommandProperty, value);
    }
    
}