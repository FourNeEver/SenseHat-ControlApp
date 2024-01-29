using MauiApp2.Services;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MauiApp2;

public partial class LedControl : ContentPage
{

    PixelData SetColors = new PixelData();

    public LedControl()
	{
		InitializeComponent();

	}


    protected override void OnAppearing()
    {
        
    }

    private void RValue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        SetColors.R = (int)e.NewValue;
        Shell.Current.Resources["LedColor"] = Color.FromRgb((int)SetColors.R, (int)SetColors.G, (int)SetColors.B);
    }

    private void GValue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        SetColors.G = (int)e.NewValue;
        Shell.Current.Resources["LedColor"] = Color.FromRgb((int)SetColors.R, (int)SetColors.G, (int)SetColors.B);
    }

    private void BValue_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        SetColors.B = (int)e.NewValue;
        Shell.Current.Resources["LedColor"] = Color.FromRgb((int)SetColors.R, (int)SetColors.G, (int)SetColors.B);
    }

    private async void Led0x0_Clicked(object sender, EventArgs e)
    {
        var item = (Button)sender;
        item.BackgroundColor = Color.FromRgb((int)SetColors.R, (int)SetColors.G, (int)SetColors.B);
        SetColors.y = int.Parse(item.CommandParameter.ToString().Last().ToString());
        SetColors.x = int.Parse(item.CommandParameter.ToString().First().ToString());
        await DataFetchService.PostLedData(SetColors);
    }

    private async void SclClear_Clicked(object sender, EventArgs e)
    {
        await DataFetchService.PostLedData();
    }

    private async void SclRefresh_Clicked(object sender, EventArgs e)
    {
        string leddata = await DataFetchService.GetLedData();
        string led = leddata;
        
    }
}