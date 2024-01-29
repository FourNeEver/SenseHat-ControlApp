using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace MauiApp2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();


    }
}
