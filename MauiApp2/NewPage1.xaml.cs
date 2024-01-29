using MauiApp2.Services;
using System.Text.RegularExpressions;

namespace MauiApp2;

public partial class NewPage1 : ContentPage
{
    string ipAdress;
    string path;
    Uri uri;
	public NewPage1()
	{
		InitializeComponent();
	}

    private void OnEntryIPChanged(object sender, TextChangedEventArgs e)
    {
        var regexItem = new Regex("\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$");
        if (!regexItem.IsMatch(e.NewTextValue))
        {
            IpError.IsVisible = true;
            Connect.IsEnabled = false;
            ipAdress = null;
        }
        else
        {
            IpError.IsVisible = false;
            Connect.IsEnabled = true;
            ipAdress = e.NewTextValue;
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {

    }

    private async void Conncect_Clicked(object sender, EventArgs e)
    {
        uri = new Uri(("http://"+ipAdress+":"+path));
        string DATA = await DataFetchService.GetData(uri);
        if (DATA != null)
        {
            StatusInd.Fill = Color.Parse("Green");
            DataFetchService.Connected();
        }
        else
        {
            StatusInd.Fill = Color.Parse("Red");
            DataFetchService.Disconnected();
        }
    }

    private void Path_TextChanged(object sender, TextChangedEventArgs e)
    {
        path = e.NewTextValue;
    }

    private void Path_Completed(object sender, EventArgs e)
    {

    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        App.Current.Resources["Delay"] = picker.SelectedItem;
    }
}