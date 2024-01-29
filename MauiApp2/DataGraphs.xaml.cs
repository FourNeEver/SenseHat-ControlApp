using MauiApp2.Services;
using System.Data;
using System.Text.Json;

namespace MauiApp2;

public partial class DataGraphs : ContentPage
{
    int time = 0;
    static DataFrame DATA = new DataFrame();
    bool is_busy = false;
    public DataGraphs()
	{
		InitializeComponent();
	}

    private async void GraphUdate()
    {

        await DataFetchService.TestConnection();
        is_busy= true;
        while (DataFetchService.Connection && is_busy)
        {
            await DataFetchService.TestConnection();
            await Task.Delay(int.Parse(App.Current.Resources["Delay"].ToString()));
            //await Task.Delay(50);
            string data = await DataFetchService.GetData();
            if (data != null)
            {
                DATA = JsonSerializer.Deserialize<DataFrame>(data);

                if (time < 600)
                {
                    time += 10;
                }
                else
                {
                    for (int i = 1; i < PitchGraph.Points.Count; i++)
                    {

                        PitchGraph.Points[i] = PitchGraph.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    PitchGraph.Points.RemoveAt(0);

                    for (int i = 1; i < YawGraph.Points.Count; i++)
                    {
                        YawGraph.Points[i] = YawGraph.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    YawGraph.Points.RemoveAt(0);

                    for (int i = 1; i < RollGraph.Points.Count; i++)
                    {
                        RollGraph.Points[i] = RollGraph.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    RollGraph.Points.RemoveAt(0);

                    for (int i = 1; i < GraphPressure.Points.Count; i++)
                    {
                        GraphPressure.Points[i] = GraphPressure.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    GraphPressure.Points.RemoveAt(0);

                    for (int i = 1; i < GraphHumidity.Points.Count; i++)
                    {
                        GraphHumidity.Points[i] = GraphHumidity.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    GraphHumidity.Points.RemoveAt(0);

                    for (int i = 1; i < GraphTemperature.Points.Count; i++)
                    {
                        GraphTemperature.Points[i] = GraphTemperature.Points[i].Offset(-10, 0);
                        //Graph.Points.Move(i, i - 1);
                    }
                    GraphTemperature.Points.RemoveAt(0);

                }

                PitchGraph.Points.Add(new Point(time, DATA.orient.pitch / (-3.6) + 100));
                YawGraph.Points.Add(new Point(time, DATA.orient.yaw / (-3.6) + 100));
                RollGraph.Points.Add(new Point(time, DATA.orient.roll / (-3.6) + 100));
                GraphPressure.Points.Add(new Point(time, DATA.pressure / (-12.6) + 100));
                GraphHumidity.Points.Add(new Point(time, DATA.humidity * (-1) + 100));
                GraphTemperature.Points.Add(new Point(time, DATA.temperatura / (-1.35) + 100));

            }
        }

    }

    protected override void OnAppearing()
    {
        //Refresh();
        base.OnAppearing();
        if(!is_busy) GraphUdate();
    }

    protected override void OnDisappearing()
    {
       base.OnDisappearing();
       DataFetchService.Disconnected();
       is_busy= false;
    }

}