using MauiApp2.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiApp2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("NewPage", typeof(NewPage1));
        Routing.RegisterRoute("RawData", typeof(RawData));
        Routing.RegisterRoute("NewPage2", typeof(NewPage2));
        Routing.RegisterRoute("DataGraphs", typeof(DataGraphs));
        Routing.RegisterRoute("LedControl", typeof(LedControl));


    }
}
