using MauiApp2.Services;
using System.Text.Json;

namespace MauiApp2;

public partial class RawData : ContentPage
{
    static DataFrame DATA = new DataFrame();
    bool is_busy = false;
    public RawData()
	{
		InitializeComponent();

        //Data.Text = DataFetchService.GetData().Result;
        //OnAppearing();
        //Point point = new Point(10, 10);
        //Point point1 = new Point(20, 10);
        //Graph.Points.Add(point);
        //Graph.Points.Add(point1);

    }
    private async void Refresh()
    {
        await DataFetchService.TestConnection();
        is_busy = true;
        string data;
        while (DataFetchService.Connection && is_busy)
        {
            if (DataFetchService.Connection)
            {
                await DataFetchService.TestConnection();
                await Task.Delay(int.Parse(App.Current.Resources["Delay"].ToString()));
                data = await DataFetchService.GetData();
                DATA = JsonSerializer.Deserialize<DataFrame>(data);
                TemperatureData.Text = DATA.temperatura.ToString();
                PressureData.Text = DATA.pressure.ToString();
                HumidityData.Text = DATA.humidity.ToString();
                PitchData.Text = DATA.orient.pitch.ToString();
                RollData.Text = DATA.orient.roll.ToString();
                YawData.Text = DATA.orient.yaw.ToString();
                if (DATA.temperatura.ToString() == null) TemperatureData.Text = "Connection Interrupt";
                if (DATA.pressure.ToString() == null) PressureData.Text = "Connection Interrupt";
                if (DATA.humidity.ToString() == null) HumidityData.Text = "Connection Interrupt";
                if (DATA.orient.ToString() == null) PitchData.Text = "Connection Interrupt";
                if (DATA.orient.ToString() == null) RollData.Text = "Connection Interrupt";
                if (DATA.orient.ToString() == null) YawData.Text = "Connection Interrupt";
            }
            else
            {
                TemperatureData.Text = "No connection";
                PressureData.Text = "No connection";
                HumidityData.Text = "No connection";
                PitchData.Text = "No connection";
                RollData.Text = "No connection";
                YawData.Text = "No connection";
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (!is_busy) Refresh();
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        DataFetchService.Disconnected();
        is_busy= false;
    }


}