namespace MauiApp2;

public class NewPage2 : ContentPage
{
	public NewPage2()
	{
		var button= new Button { Text = "Go Back Home" };
        button.Clicked += Button_Clicked;
		Content = new VerticalStackLayout
		{
			Children = {
				button,
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI! from Code"
				}
			}
		};
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("..");
    }
}